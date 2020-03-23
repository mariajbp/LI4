from flask import Flask, request, jsonify, make_response
from flask_restful import Resource, Api
from flask_sqlalchemy import SQLAlchemy
from werkzeug.security import generate_password_hash, check_password_hash
import jwt
import datetime

app = Flask(__name__)
api = Api(app)

app.config['SECRET_KEY'] = 'hehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehe'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:root@localhost/ticketnow'

db = SQLAlchemy(app)



class User(db.Model):
    __tablename__ = "User"
    id_user = db.Column(db.String(10), primary_key=True)
    email = db.Column(db.String(320))
    password_hash = db.Column(db.String(200))
    name = db.Column(db.String(100))


@app.route('/login')
def login():
    auth = request.authorization

    #print(generate_password_hash("epah_mas_que_chatice"))

    if not auth or not auth.username or not auth.password:
        return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

    user = User.query.filter_by(id_user=auth.username).first()

    if not user:
        return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

    if check_password_hash(user.password_hash , auth.password):
        token = jwt.encode({'username' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=30)}, app.config['SECRET_KEY'])
        return jsonify({'token' : token.decode('UTF-8')})

    

    return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

if __name__ == '__main__':
    #app.run(host='0.0.0.0',debug=True,ssl_context='adhoc')
    app.run(host='0.0.0.0',debug=True)