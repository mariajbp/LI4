from common.app_init import db
from common.error import ErrorCode, ErrorCodeException
import datetime
from binascii import hexlify , unhexlify

class Transaction(db.Model):
    __tablename__ = "Transaction"
    id_transaction = db.Column(db.String(18), primary_key=True)
    id_user = db.Column(db.String(10), )
    id_ticket = db.Column(db.BINARY(32), nullable=False)
    total_price = db.Column(db.Float(), nullable=False)
    datetime = db.Column(db.DateTime(), default=datetime.datetime.utcnow)
    

    def __init__(self,id_transaction,id_user,id_ticket,total_price,datetime=None):
        self.id_transaction = id_transaction
        self.id_user = id_user
        self.id_ticket = id_ticket
        self.total_price = total_price
        self.datetime = datetime


    @staticmethod
    def get_transaction(id_transaction,id_ticket):
        e = Transaction.query.filter_by(id_transaction=id_transaction,id_ticket=unhexlify(id_ticket)).first()
        if e == None:
            raise ErrorCodeException(ErrorCode.TRANSACTION_DOESNT_EXISTS)
        return e


    @staticmethod
    def add_transaction(transaction):
        e = Transaction.query.filter_by(id_transaction=transaction.id_transaction, id_ticket=transaction.id_ticket).first()
        if e != None:
            raise ErrorCodeException(ErrorCode.TRANSACTION_EXISTS)
        
        db.session.add(transaction)
        db.session.commit()