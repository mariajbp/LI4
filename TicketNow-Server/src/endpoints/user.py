from flask_restful import Resource , reqparse
from flask import request
from model.users import User
from flask_jwt_extended import jwt_required , get_jwt_identity
from common.utils import ErrorCodeException

class UserAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_user', type=str, required=False, help='User Identifier')
    
    @jwt_required
    def get(self):  
        args = UserAPI.parser.parse_args()
        id_user = args['id_user']
        print(id_user)
        return { "users" : [ u.to_json() for u in User.get_all() ] } if not id_user  \
            else { "user" : User.get_user(id_user).to_json() }      

    def post(self):
        body = request.json
        if not body:
            return { "error" : "No json body found on request" }
        try:
            User.add_user(User(body['id_user'],body['email'],body['password'],body['name']))
            return { "message" : "success" }
        except ErrorCodeException as ec:
            return { "error" : ec.message() }

    @jwt_required
    def delete(self):
        args = UserAPI.parser.parse_args()
        id_user = args['id_user']

        if not id_user:
            return { "error" : "Argument required" }

        try:
            User.delete(id_user)
            return { "message" : "success" }
        except ErrorCodeException as ec:
            return { "error" : ec.message() }
        

""" {
	"id_user" : "a11111",
	"email" : "a11111@alunos.uminho.pt",
	"password" : "password segura" ,
	"name" : "Jubilado HEHE"
} """