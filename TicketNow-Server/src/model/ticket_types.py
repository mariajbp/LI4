from common.app_init import db
from common.error import ErrorCode, ErrorCodeException


class TicketType(db.Model):
    __tablename__ = "TicketType"
    type = db.Column(db.Integer(), primary_key=True)
    price = db.Column(db.Float(), nullable=False)
    name = db.Column(db.String(25), nullable=False, unique=True)
    

    def __init__(self,type,price,name):
        self.type = type
        self.price = price
        self.name = name

    @staticmethod
    def get_type(type):
        tt = TicketType.query.filter_by(type=type).first()
        if tt == None:
            raise ErrorCodeException(ErrorCode.TICKETTYPE_DOESNT_EXISTS)
        return tt

    @staticmethod
    def get_all():
        return TicketType.query.all()

    @staticmethod
    def add_type(type):
        try:
            TicketType.get_type(type.type)
        except ErrorCodeException as ec:
            db.session.add(type)
            db.session.commit()
            return
        raise ErrorCodeException(ErrorCode.TICKETTYPE_EXISTS)

        


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