""" from flask import Flask 
from flask_restful import Api , Resource

app = Flask(__name__)
api = Api(app)

class UserAPI(Resource):
    
    def get(self):
        return { "message" : "Ola mundo" }

    def get(self,photos):
        return { "message" : "Ola " + photos }

api.add_resource(UserAPI, '/user',endpoint="User1")
api.add_resource(UserAPI, '/user/<photos>',endpoint="User2")

if __name__ == '__main__':
    app.run(host='0.0.0.0',debug=True) """


#matrix = []
#for i in range(14):
#    matrix.append([])
#    for j in range(14):
#        matrix[i].append(i*j)
#
#for e in matrix:
#    print(e)



def populate_meals(num=10):
    # importing the requests library 
    import requests
    from datetime import date , timedelta
    
    # defining the api-endpoint  
    API_ENDPOINT = "http://localhost:5000/api/meal"
    
    # your API key here 
    AUTH_TOKEN = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1ODgyNTE1ODAsIm5iZiI6MTU4ODI1MTU4MCwianRpIjoiMzViMTk5NjgtMTg1MS00Y2JiLTkxN2UtNjczNWUzYTkzNDQ1IiwiZXhwIjoxNjE5Nzg3NTgwLCJpZGVudGl0eSI6ImExMjM0NSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyIsInVzZXJfY2xhaW1zIjp7InBlcm1pc3Npb25zIjo1fX0._-ZUM1O5Iy6f-N8f7WjCti8n0JGIfH6jt4ejpdvwBmQ"
    hed = {'Authorization': 'Bearer ' + AUTH_TOKEN}
    
    
    tdy = date.today()
    for day in range(num):
        timedelta(days=day)
        for mt in ("dinner","lunch"):
            data = {
	            "date" : str(tdy + timedelta(days=day)),
	            "meal_type" : mt,
	            "location" : "gualtar",
	            "main_dish" : "É FRANGO! FESTEJA CALOIRO!",
	            "soup" : None,
	            "description" : None
            } 
            # sending post request and saving response as response object 
            r = requests.post(url = API_ENDPOINT, json = data, headers=hed)
            response = r.json()
            print(response)

#populate_meals(10)