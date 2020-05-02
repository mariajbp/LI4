from flask_restful import Resource , reqparse
from flask import request
from model.meal import Meal , MealType, Location
from flask_jwt_extended import jwt_required , get_jwt_identity
from common.utils import content_provider_required , user_required , Permissions
from common.error import ErrorCodeException
from common.responses import success , error_code , forbidden , error_message


class MealAPI(Resource):
    parser_get = reqparse.RequestParser()
    parser_get.add_argument('begin', type=str, required=False, help='Inital date')
    parser_get.add_argument('end', type=str, required=False, help='End date')
    parser_get.add_argument('location', type=str, required=False, help='Location')
    parser_get.add_argument('meal_type', type=str, required=False, help='Type of the meal')

    parser_del = reqparse.RequestParser()
    parser_del.add_argument('date', type=str, required=True, help='Date')
    parser_del.add_argument('location', type=str, required=True, help='Localtion')
    parser_del.add_argument('meal_type', type=str, required=True, help='Type of the meal')
    

    @user_required
    def get(self):
        import datetime
        
        args = MealAPI.parser_get.parse_args()
        try:
            begin = datetime.date.today() if args['begin'] == None else datetime.date.fromisoformat(args['begin'])
            end = begin + datetime.timedelta(weeks=1) if args['end'] == None else datetime.date.fromisoformat(args['end'])
        except:
            return error_message("Invalid date format (ISO Date format required)!") , 400

        if begin > end:
            return error_message("begin bigger than end!") , 400
            
        try:
            id_meal_type = None
            if args['meal_type'] != None:
                id_meal_type = MealType.get_by_name(args['meal_type']).id_meal_type  
            id_location = None
            if args['location'] != None:
                id_location = Location.get_by_name(args['location']).id_location
            if id_location != None or id_meal_type != None:
                print("here")
                return { "meals" : [ m.to_json() for m in Meal.get_between(begin,end,id_meal_type,id_location) ] } , 200
        except ErrorCodeException as ec:
            return error_code(ec) , 400

        

        print("or here here")
        return { "meals" : [ m.to_json() for m in Meal.get_between(begin,end) ] } , 200


    @content_provider_required
    def post(self):
        import datetime

        body = request.json
        if not body:
            return { "error" : "No json body found on request" }
        
        # Request body argument validation
        date = None
        try:
            date = datetime.date.fromisoformat(body['date'])
        except:
            return error_message("Invalid date format (ISO Date format required)!") , 400

        try:
            id_meal_type = MealType.get_by_name(body['meal_type']).id_meal_type
            id_location = Location.get_by_name(body['location']).id_location
            try:
                Meal.add_meal(Meal(date,id_location,id_meal_type,body['soup'],body['main_dish'],body['description']))
                return success()
            except KeyError:
                return error_message("Missing arguments!") , 400
            #except MySQLdb._exceptions.DataError:
            #    return error_message("Invalid argument length!") , 400
            except Exception as e:
                return error_message("Something unexpected occured!") , 500
        
        except ErrorCodeException as ec:
            return error_code(ec) , 400

        
        


    @content_provider_required
    def delete(self):
        args = MealAPI.parser_del.parse_args()
        date = args['date']
        location = args['location']
        meal_type = args['meal_type']

    
    
        if not (date and meal_type and location):
            return error_message("Arguments required") , 400
    
        try:
            id_location = Location.get_by_name(location).id_location
            id_meal_type = MealType.get_by_name(meal_type).id_meal_type
            Meal.delete(date, id_location, id_meal_type)
            
            return success()
        except ErrorCodeException as ec:
            return error_code(ec) , 400
        

    #@content_provider_required
    #def put(self):
    #    id_user = get_jwt_identity()
    #    args = UserAPI.parser_put.parse_args()
    #    old_password = args['old_password']
    #    new_password = args['new_password']

    #    if old_password and new_password : 
    #        print(id_user)
    #        return success()
    #    else:
    #        return { "error" : "Argument required" }