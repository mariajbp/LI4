from flask import request, jsonify, make_response
from flask_restful import Resource
from werkzeug.security import generate_password_hash, check_password_hash
import jwt
import datetime
from functools import wraps

from common.app_init import app , api , db
from common.utils import auth_required
from endpoints.auth import LoginAPI , LogoutAPI , RegisterAPI
from endpoints.user import UserAPI , UserInfoAPI 
from endpoints.ticket import TicketAPI
from endpoints.user_ticket import UserTicketAPI
from endpoints.ticket_type import TicketTypeAPI
from endpoints.validator import ValidatorAPI
from endpoints.kiosk import KioskAPI
from endpoints.history import UserHistoryAPI
from endpoints.meal import MealAPI
from endpoints.statistis import StatisticsAPI, MyStatisticsAPI

# Used for populate
from model.users import User
from model.ticket_types import TicketType
from model.tickets import Ticket

#########################################################################    

base_endpoint = app.config['BASE_ENDPOINT']

api.add_resource(UserAPI, base_endpoint + '/user')
api.add_resource(UserInfoAPI, base_endpoint + '/user/<id_user>')
api.add_resource(TicketAPI, base_endpoint + '/ticket')
api.add_resource(LoginAPI, base_endpoint + '/login')
api.add_resource(LogoutAPI, base_endpoint + '/logout')
api.add_resource(UserTicketAPI, base_endpoint + '/user/<id_user>/tickets')
api.add_resource(TicketTypeAPI, base_endpoint + '/ticket/type')
api.add_resource(ValidatorAPI, base_endpoint + '/validator')
api.add_resource(KioskAPI, base_endpoint + '/kiosk/ticket')
api.add_resource(UserHistoryAPI, base_endpoint + '/user/<id_user>/history')
api.add_resource(MealAPI, base_endpoint + '/meal')
api.add_resource(RegisterAPI, base_endpoint + '/register')
api.add_resource(StatisticsAPI, base_endpoint + '/statistics/global')
api.add_resource(MyStatisticsAPI, base_endpoint + '/statistics/me')


#########################################################################

def populate():
    from binascii import unhexlify
    # Insert some users
    try:
        User.add_user(User("a12345","a12345@alunos.uminho.pt","epah_mas_que_chatice","António Barosa",permissions=5))
    except:
        pass
    try:
        User.add_user(User("a11111","a11111@alunos.uminho.pt","epah_mas_que_chatice","Adelino Costa"))
    except:
        pass
    try:
        User.add_user(User("a22222","a22222@alunos.uminho.pt","epah_mas_que_chatice","Rosa Matilde"))
    except:
        pass
    try:
        User.add_user(User("a33333","a33333@alunos.uminho.pt","epah_mas_que_chatice","Fernando Magalhães"))
    except:
        pass 
        
    # Insert some ticket types
    try:
        TicketType.add_type(TicketType(1,1.8,"simple"))
    except:
        pass
    try:
        TicketType.add_type(TicketType(2,2.5,"complete"))
    except:
        pass

    
    from model.meal import MealType , Location , Meal
    try:
        MealType.add_meal_type(MealType("lunch"))
    except:
        pass
    try:
        MealType.add_meal_type(MealType("dinner"))
    except:
        pass
    try:
        MealType.add_meal_type(MealType("lunch_veg"))
    except:
        pass
    try:
        MealType.add_meal_type(MealType("dinner_veg"))
    except:
        pass

    try:
        Location.add_location(Location("azurem"))
    except:
        pass
    try:
        Location.add_location(Location("gualtar"))
    except:
        pass

    from datetime import date
    try:
        Meal.add_meal(Meal(date.fromisoformat('2019-12-04'),1,1,soup="Sopa de Frango",main_dish="Frango"))
    except:
        pass
    # Insert some tickets
    #try:
    #    Ticket.add_ticket(Ticket("a12345",1,unhexlify('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'))) 
    #except:
    #    pass
    #try:
    #    Ticket.add_ticket(Ticket("a12345",1,unhexlify('aaaaaaaaaaaaaaabaaaaaaaaaaaaaaabaaaaaaaaaaaaaaabaaaaaaaaaaaaaaab'))) 
    #except:
    #    pass
    #try:
    #    Ticket.add_ticket(Ticket("a12345",1,unhexlify('aaaaaaaaaaaaaaacaaaaaaaaaaaaaaabaaaaaaaaaaaaaaacaaaaaaaaaaaaaaab')))
    #except:
    #    pass
    #try:
    #    Ticket.add_ticket(Ticket("a12345",2,unhexlify('bbbbbbbbbbbbbbbbaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbaaaaaaaaaaaaaaab')))
    #except:
    #    pass
#
    #try:
    #    from model.tickets import set_as_used
    #    set_as_used(unhexlify('bbbbbbbbbbbbbbbbaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbaaaaaaaaaaaaaaab'),"a12345")
    #except:
    #    pass

    

populate()

from hashlib import sha256
if __name__ == '__main__':
    app.run(host='0.0.0.0',debug=True)
    
    
#   app.run(host='0.0.0.0',debug=True,ssl_context='adhoc')