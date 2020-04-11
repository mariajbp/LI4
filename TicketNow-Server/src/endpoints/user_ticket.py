from flask_restful import Resource , reqparse
from flask import request
from model.tickets import Ticket
from model.users import User
from flask_jwt_extended import get_jwt_identity
from common.utils import auth_required
from common.error import ErrorCodeException
#from common.responses import success , error_code

class UserTicketAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('id_ticket', type=str, required=False, help='Ticket Identifier')

    @auth_required
    def get(self,id_user):
        args = UserTicketAPI.parser.parse_args()
        
        target_id_ticket = args['id_ticket']
        
        sender_user = User.get_user(get_jwt_identity())

        # Checks if id_user has been specified
        if target_id_ticket != None:
            try:
                t = Ticket.get_ticket(target_id_ticket)
                if t.id_user == sender_user.id_user:
                    return { "ticket" : Ticket.get_ticket(target_id_ticket).to_json() }
            except ErrorCodeException as ec:
                return error_code(ec)

        else:
            return { "owned_tickets" : [ t.to_json() for t in Ticket.get_not_used(id_user=sender_user.id_user) ] }