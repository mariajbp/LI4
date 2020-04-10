from common.app_init import db
from common.utils import ErrorCode, ErrorCodeException


class History(db.Model):
    __tablename__ = "TicketType"
    used_datetime = db.Column(db.DATETIME(), primary_key=True)
    id_ticket = db.Column(db.String(16), nullable=False)
    id_user = db.Column(db.String(10), primary_key=True)
    

    def __init__(self,used_datetime,id_ticket,name):
        self.type = type
        self.price = price
        self.name = name

    @staticmethod
    def get_type(type):
        return History.query.filter_by(type=type).first()

    @staticmethod
    def get_all():
        return History.query.all()

    @staticmethod
    def add_type(type):
        
        if History.get_user(type.id_user):
            raise ErrorCodeException(ErrorCode.TICKETTYPE_EXISTS)

        db.session.add(type)
        db.session.commit()


    @staticmethod
    def delete(id_ticket):
        t = TicketType.query.filter(TicketType.id_ticket==id_ticket)
        
        if t.delete() == 1:
            db.session.commit()
        else:
            raise ErrorCodeException(ErrorCode.TICKETTYPE_DOESNT_EXISTS)



    def to_json(self):
        return { 
            "type" : self.type,
            "price" : self.price,
            "name" : self.name
        }