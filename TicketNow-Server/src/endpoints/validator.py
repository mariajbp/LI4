from flask_restful import Resource , reqparse
from flask import request
#from model.tickets import Ticket
#from model.users import User
#from flask_jwt_extended import get_jwt_identity
from common.utils import validator_required
from common.error import ErrorCodeException
from common.responses import error_message

from model.tickets import set_as_used


class ValidatorAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_ticket', type=str, required=True, help='Ticket Identifier')
    parser.add_argument('id_user', type=str, required=True, help='User Identifier')


    @validator_required
    def post(self):
        args = ValidatorAPI.parser.parse_args()
        # Argumentos sao necessarios
        
        target_id_user = args['id_user']
        target_id_ticket = args['id_ticket']

        #if target_id_user == None or target_id_ticket == None :
        #    return error_message("Insuficient arguments") , 400

        try:
            set_as_used(target_id_ticket,target_id_user)
            return { "status" : 1 }
        except ErrorCodeException as ec:
            return { 
                "status" : 0,
                "error" : ec.message()
            } , 200