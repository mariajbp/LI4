from flask import Flask, request, jsonify, make_response
from flask_restful import Resource, Api
from flask_sqlalchemy import SQLAlchemy
from werkzeug.security import generate_password_hash, check_password_hash
import jwt
import datetime

from functools import wraps

app = Flask(__name__)
api = Api(app)

app.config['TOKEN_GEN_KEY'] = '1430b4f944d890bd4eed14421e5a31b595d3e11d12fd59fb4c419c2f909045ef'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:root@localhost/ticketnow'

db = SQLAlchemy(app)



class User(db.Model):
    __tablename__ = "User"

    id_user = db.Column(db.String(10), primary_key=True)
    email = db.Column(db.String(320))
    password_hash = db.Column(db.String(200))
    name = db.Column(db.String(100))


""" class UserAPI(Resource):
    def get(self):
        if() """

    
def auth_required(f):
    @wraps(f)
    def api_method(*args, **kwargs):
        token = request.args.get('token')

        
        #if 'x-access-token' in request.headers:
        #    token = request.headers['x-access-token']

        print(token)

        if not token:
            print("Debug 1")
            return jsonify({'message' : 'Token is missing!'}), 401

        try: 
            print("Debug 2")
            data = jwt.decode(token, app.config['TOKEN_GEN_KEY'])
            current_user = User.query.filter_by(id_user=data['id_user']).first()
        except:
            return jsonify({'message' : 'Token is invalid!'}), 401

        print("Debug 3")
        return f(current_user, *args, **kwargs)

    return api_method


@app.route('/hello')
@auth_required
def hello_world(current_user):
    return jsonify({'message' : 'Hello {}!'.format(current_user.id_user)}), 200
    


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
        token = jwt.encode({'id_user' : auth.username, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['TOKEN_GEN_KEY'])
        return jsonify({'token' : token.decode('UTF-8')})

    

    return make_response('Could not verify', 401, {'WWW-Authenticate' : 'Basic realm="Login required!"'})

if __name__ == '__main__':
    #app.run(host='0.0.0.0',debug=True,ssl_context='adhoc')
    app.run(host='0.0.0.0',debug=True)