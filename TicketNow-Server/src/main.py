from flask import Flask, request, jsonify, make_response
from flask_restful import Resource, Api
from flask_sqlalchemy import SQLAlchemy
from werkzeug.security import generate_password_hash, check_password_hash
import jwt
import datetime

app = Flask(__name__)
api = Api(app)

app.config['SECRET_KEY'] = 'hehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehehe'
app.config['SQLALCHEMY_DATABASE_URI'] = "mysql://dss:dss_admin@localhost/db"

db = SQLAlchemy(app)

""" class HelloWorld(Resource):
    def get(self):
        
        return {'hello' : 'world'}

api.add_resource(HelloWorld, '/')


class User(Resource):
    def get(self):
        pass """


class User(db.Model):
    id_user = db.Column(db.String(10), primary_key=True)
    email = db.Column(db.String(320))
    password_hash = db.Column(db.String(64))

    name = db.Column(db.String(50))
    password = db.Column(db.String(80))
    admin = db.Column(db.Boolean)


@app.route('/login')
def login():
    auth = request.authorization

    if not auth or not auth.username or not auth.password:
        return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

    if check_password_hash(generate_password_hash("asd"), auth.password):
        token = jwt.encode({'username' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=30)}, app.config['SECRET_KEY'])

        return jsonify({'token' : token.decode('UTF-8')})

    return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

if __name__ == '__main__':
    #app.run(host='0.0.0.0',debug=True,ssl_context='adhoc')
    app.run(host='0.0.0.0',debug=True)