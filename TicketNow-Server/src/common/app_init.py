from flask import Flask , jsonify
from flask_restful import Api
from flask_sqlalchemy import SQLAlchemy
from flask_jwt_extended import JWTManager
from werkzeug.exceptions import HTTPException
from common.responses import unauthorized
from model.session_table import SessionTable
from common.config import *

app = Flask(__name__)
api = Api(app)

jwt = JWTManager(app)
jwt.invalid_token_loader(unauthorized)

app.config['JWT_SECRET_KEY'] = jwt_secret_key
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://{}:{}@localhost/ticketnow'.format(db_username,db_password)
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['BASE_ENDPOINT'] = '/api'
app.config['JWT_BLACKLIST_ENABLED'] = True
app.config['JWT_BLACKLIST_TOKEN_CHECKS'] = ['access', 'refresh']

db = SQLAlchemy(app)

dev_jti_whitelist = [ 
    "35b19968-1851-4cbb-917e-6735e3a93445",
    "204bf68c-46fc-4751-8acc-7308fc7ad4cb",

    "fceda392-a2f1-4efd-90a4-726f309cfd04",

    "16a47149-72de-457e-9336-0b59eded799f",
    "dc15bbb4-25bf-442d-a9ad-fc555a57d063" 
    ]


# Reuse of token blacklist for token-session validattion
@jwt.token_in_blacklist_loader
def check_if_token_in_blacklist(decoded_token):
    t_jti = decoded_token['jti']
    return not (SessionTable.contains(decoded_token['identity'],t_jti) or t_jti in dev_jti_whitelist)



#@jwt.expired_token_loader
#def my_expired_token_callback(expired_token):
#    token_type = expired_token['type']
#    return jsonify({
#        'status': 401,
#        'sub_status': 42,
#        'msg': 'Token has been revoked'.format(token_type)
#    }), 401