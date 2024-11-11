# Main file in charge of running the flask app and handling the routes
from flask import Flask, render_template, request, redirect, url_for, jsonify
from flask_sqlalchemy import SQLAlchemy
from datetime import datetime, timezone
from bcrypt import hashpw, gensalt

app = Flask(__name__)

app.debug = True

app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///test.db'

db = SQLAlchemy(app)

class User(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(200), nullable=False)
    email = db.Column(db.String(200), nullable=False)
    passwordHash = db.Column(db.String(200), nullable=False)
    date_created = db.Column(db.DateTime, default=datetime.now(timezone.utc))
    
    def __repr__(self):
        return '<User %r>' % self.id
    
class Session(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    userId = db.Column(db.Integer, db.ForeignKey('user.id'), nullable=False)
    date_created = db.Column(db.DateTime, default=datetime.now(timezone.utc))
    
    def __repr__(self):
        return '<Session %r>' % self.id
    
@app.route('/auth/register', methods=['POST'])
def register():
    if request.method == 'POST':
        data = request.json
        name = data['name']
        email = data['email']
        password = data['password']
        user = User(name=name, email=email, passwordHash=hashpw(password.encode('utf-8'), gensalt()).decode('utf-8'))
        db.session.add(user)
        db.session.commit()

        return jsonify({'success': 'User created successfully'})
    
@app.route('/auth/login', methods=['POST'])
def login():
    if request.method == 'POST':
        data = request.json
        email = data['email']
        password = data['password']
        user = User.query.filter_by(email=email).first()
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
    
if __name__ == '__main__':
    app.run()