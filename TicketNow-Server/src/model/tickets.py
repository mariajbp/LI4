from app import db

class Ticket(db.Model):
    __tablename__ = "User"
    
    id_ticket = db.Column(db.Integer(10), primary_key=True)
    price = db.Float

    email = db.Column(db.String(320))
    password_hash = db.Column(db.String(200))
    name = db.Column(db.String(100))

    def get_user(id_user):
        return Users.query.filter_by(id_user=id_user).first()



""" 
class Ticket(db.Model):
    __tablename__ = "User"
    
    id_ticket = db.Column(db.Integer(10), primary_key=True)
    price = db.Float 
"""