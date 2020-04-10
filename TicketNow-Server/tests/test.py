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


matrix = []
for i in range(14):
    matrix.append([])
    for j in range(14):
        matrix[i].append(i*j)

for e in matrix:
    print(e)