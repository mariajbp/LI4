#from flask_restful import Resource , reqparse
#from flask import request
#from model.history import History
#from flask_jwt_extended import get_jwt_identity
#from common.utils import auth_required
#from common.error import ErrorCodeException
#from common.responses import error_code , unauthorized
#from common.utils import user_required




class StatisticsAPI(Resource):


    def get(self):
        pass
        """ 
        sender_id_user = get_jwt_identity()
        if sender_id_user != id_user:
            return unauthorized("Forbiden!"), 403
        try:
            user_hist = History.get_all(id_user)
            return { "history" : [ he.to_json() for he in user_hist ] if user_hist else [] } , 200
        except ErrorCodeException as ec:
            return error_code(ec) , 500 
        """
        # SELECT DATE(used_datetime) as used_date,id_user FROM History WHERE DATE(used_datetime) > '2020-05-01' AND DATE(used_datetime) < '2020-06-03';

    # TODO: Add the rest of the methods POST PUT DELETE