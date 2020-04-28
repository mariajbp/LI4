from flask_restful import Resource , reqparse
from flask import request
from model.tickets import Ticket
from model.ticket_types import TicketType
#from model.users import User
from flask_jwt_extended import get_jwt_identity
from common.utils import user_required
from common.error import ErrorCodeException
from common.responses import success , error_code , error_message

class KioskAPI(Resource):
    __MAX_TICKET_AMOUNT = 100

    parser_get = reqparse.RequestParser()
    parser_get.add_argument('ticket_type', type=int, required=True, help='Ticket Type Identifier')

    parser_post = reqparse.RequestParser()
    parser_post.add_argument('ticket_type', type=int, required=True, help='Ticket Type Identifier')
    parser_post.add_argument('amount', type=int, required=True, help='Amount of tickets to buy')

    #@user_required
    #def get(self):
    #    args = KioskAPI.parser_post.parse_args()
    #    
    #    ticket_type = args['ticket_type']
#
    #    try:
    #        tt = TicketType.get_type(ticket_type)
    #    
    #    
    #    except ErrorCodeException as ec:
    #        return error_code(ec) , 500



    @user_required
    def post(self):
        args = KioskAPI.parser_post.parse_args()

        target_id_user = get_jwt_identity()

        amount = args['amount']
        ticket_type = args['ticket_type']
        
        from binascii import hexlify
        bought = []

        price = 0.0
        try:
            price = TicketType.get_type(ticket_type).price
        except ErrorCodeException as ec:
            return error_code(ec) , 500
        
        if amount > self.__MAX_TICKET_AMOUNT or amount <= 0:
            return error_message("Invalid amount MAX = " + str(__MAX_TICKET_AMOUNT)) , 500
        
        try:
            for i in range(amount):
                tckt = Ticket(target_id_user,ticket_type)
                #print(str(tckt.id_ticket))

                bought.append(hexlify(tckt.id_ticket).decode('ascii'))
                Ticket.add_ticket(tckt)
        except ErrorCodeException as ec:
            return error_code(ec) , 500
        except :
            return error_message("Something unexpected occurred") , 500
        
        
        return { 
            "transaction_status" : "Success",
            "price" : price*amount,
            "ticket_ids" : bought
            } , 200