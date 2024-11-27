# Main file in charge of running the flask app and handling the routes
from flask import Flask, render_template, request, redirect, url_for, jsonify
from flask_sqlalchemy import SQLAlchemy
from datetime import datetime, timezone
from bcrypt import hashpw, gensalt
import uuid

app = Flask(__name__)

app.debug = True

app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///test.db'

db = SQLAlchemy(app)


class User(db.Model):
    id = db.Column(db.String(200), primary_key=True, default = str(uuid.uuid4()))
    username = db.Column(db.String(200), nullable=False)
    passwordHash = db.Column(db.String(200), nullable=False)
    cardId = db.Column(db.String(200), nullable=True)
    tickets = db.Column(db.Integer, default=0)
    credits = db.Column(db.Integer, default=0)
    date_created = db.Column(db.DateTime, default=datetime.now(timezone.utc))
    
    def __repr__(self):
        return f"{self.username}: {self.id}, {self.passwordHash}, {self.cardId}, {self.tickets}, {self.credits}, {self.date_created}"
    
class Session(db.Model):
    id = db.Column(db.String(200), primary_key=True, default = str(uuid.uuid4()))
    userId = db.Column(db.String(200), db.ForeignKey('user.id'), nullable=False)
    date_created = db.Column(db.DateTime, default=datetime.now(timezone.utc))
    
    def __repr__(self):
        return f"Session for user {self.userId}: {self.id}, {self.date_created}"
    
@app.route('/debug')
def printAll():
    users = User.query.all()
    sessions = Session.query.all()
    print("Users:")
    for user in users:
        print(user)
    print("Sessions:")
    for session in sessions:
        print(session)
    return "Check console for output"
    
@app.route('/auth/register', methods=['POST'])
def register():
    if request.method == 'POST':
        data = request.json
        username = data['username']
        email = data['email']
        password = data['password']
        user = User(username=username, email=email, passwordHash=hashpw(password.encode('utf-8'), gensalt()).decode('utf-8'))
        db.session.add(user)
        db.session.commit()

        return jsonify({'success': 'User created successfully'})
    
@app.route('/auth/login', methods=['POST'])
def login():
    if request.method == 'POST':
        data = request.json
        username = data['username']
        password = data['password']
        user = User.query.filter_by(username=username).first()
        userCheck = Session.query.filter_by(userId=user.id).first()
        if userCheck:
            db.session.delete(userCheck)
            db.session.commit()
        if user and hashpw(password.encode('utf-8'), user.passwordHash.encode('utf-8')) == user.passwordHash.encode('utf-8'):
            session = Session(userId=user.id)
            db.session.add(session)
            db.session.commit()
            return jsonify({'sessionId': session.id})
        else:
            return jsonify({'error': 'Invalid email or password'})
        
@app.route('/auth/logout', methods=['POST'])
def logout():
    if request.method == 'POST':
        data = request.json
        session = Session.query.filter_by(id=data['sessionId']).first()
        db.session.delete(session)
        db.session.commit()
        return jsonify({'success': 'User logged out successfully'})
    
def check_session(sessionId):
    session = Session.query.filter_by(id=sessionId).first()
    if session:
        return True
    else:
        return False

@app.route('/user', methods=['GET'])
def getUserData():
    args = request.args
    sessionInside = Session(id=args.get("sessionId"))
    user = User(id=sessionInside.userId)
    exists = db.session.query(db.exists().where(User.id == sessionInside.userId)).scalar()
    if (exists == False):
        return jsonify({'error': 'session not found'}) 
    print(user)
    return jsonify({'userId': user.id, 'tickets': user.tickets, 'credits': user.credits})
    
if __name__ == '__main__':
    with app.app_context():
        db.create_all()
    app.run()