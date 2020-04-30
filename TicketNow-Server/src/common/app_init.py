from flask import Flask , jsonify
from flask_restful import Api
from flask_sqlalchemy import SQLAlchemy
from flask_jwt_extended import JWTManager
from werkzeug.exceptions import HTTPException
from common.responses import unauthorized
from model.session_table import SessionTable

app = Flask(__name__)
api = Api(app)

jwt = JWTManager(app)
jwt.invalid_token_loader(unauthorized)

app.config['JWT_SECRET_KEY'] = '1430b4f944d890bd4eed14421e5a31b595d3e11d12fd59fb4c419c2f909045ef'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:root@localhost/ticketnow'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['BASE_ENDPOINT'] = '/api'
#app.config['JWT_BLACKLIST_ENABLED'] = True
#app.config['JWT_BLACKLIST_TOKEN_CHECKS'] = ['access', 'refresh']

db = SQLAlchemy(app)

dev_tokens_whitelist = [ 
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE1ODAsIm5iZiI6MTU4ODI1MTU4MCwianRpIjoiMzViMTk5NjgtMTg1MS00Y2JiLTkxN2UtNjczNWUzYTkzNDQ1IiwiZXhwIjoxNjE5Nzg3NTgwLCJpZGVudGl0eSI6ImExMjM0NSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjo1fX0._-ZUM1O5Iy6f-N8f7WjCti8n0JGIfH6jt4ejpdvwBmQ",
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE0MzMsIm5iZiI6MTU4ODI1MTQzMywianRpIjoiMjA0YmY2OGMtNDZmYy00NzUxLThhY2MtNzMwOGZjN2FkNGNiIiwiZXhwIjoxNjE5Nzg3NDMzLCJpZGVudGl0eSI6ImExMTExMSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjoxfX0.1HPkWdG55joGPv6_fBZYXXmVa57An32csYHSxYYM0ZU"
    ]


# Reuse of token blacklist for token-session validattion
@jwt.token_in_blacklist_loader
def check_if_token_in_blacklist(decoded_token):
    return not SessionTable.contains(decoded_token['identity'],decoded_token['jti'])



#@jwt.expired_token_loader
#def my_expired_token_callback(expired_token):
#    token_type = expired_token['type']
#    return jsonify({
#        'status': 401,
#        'sub_status': 42,
#        'msg': 'Token has been revoked'.format(token_type)
#    }), 401