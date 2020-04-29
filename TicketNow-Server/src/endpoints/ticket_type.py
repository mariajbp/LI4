from flask_restful import Resource , reqparse
from flask import request
from model.ticket_types import TicketType
#from flask_jwt_extended import get_jwt_identity
from common.utils import user_required
from common.error import ErrorCodeException
from common.responses import error_code

class TicketTypeAPI(Resource):
    parser = reqparse.RequestParser()
    parser.add_argument('ticket_type', type=int, required=False, help='Ticket Type Identifier')

    @user_required
    def get(self):
        args = TicketTypeAPI.parser.parse_args()
        
        target_ticket_type = args['ticket_type']
        

        # Checks if ticket_type has been specified
        if target_ticket_type != None:
            try:
                return { "ticket_type" : TicketType.get_type(target_ticket_type).to_json() }
            except ErrorCodeException as ec:
                return error_code(ec)

        else:
            return { "ticket_types" : [ tt.to_json() for tt in TicketType.get_all() ] }


    # TODO: Add the rest of the methods POST PUT DELETE