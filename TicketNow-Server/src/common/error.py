from enum import Enum , unique


""" class ErrorCode(Enum):
    __RANGE_OFFSET__ = 100

    UNKNOWN = 0
    
    class User(Enum):
        DEFAULT = 100
        EXISTS = 101
        DOESNT_EXISTS = 102
        WRONG_PASSWORD = 103

    class Ticket(Enum):
        DEFAULT = 200
        VALIDATED = 201
        DOESNT_EXISTS = 202

    def message(self):
        
        ERROR_MESSAGES = [
            [
                "Unknown error"
            ],
            [
                "Default User error",
                "User already exists",
                "User does not exists",
                "Wrong password"
            ],
            [
                "Default Ticket error",
                "Ticket already validated",
                "Ticket does not exists"
            ],
        ]
        
        return ERROR_MESSAGES[ self.__RANGE_OFFSET__ // self.value ][ self.__RANGE_OFFSET__ % self.value ] """

@unique
class ErrorCode(Enum):
    UNKNOWN = 0
    
    USER_EXISTS = 1
    USER_DOESNT_EXISTS = 2
    WRONG_PASSWORD = 3
    
    TICKET_EXISTS = 4
    TICKET_DOESNT_EXISTS = 5

    TICKETTYPE_EXISTS = 6
    TICKETTYPE_DOESNT_EXISTS = 7

    HISTORY_ENTRY_EXISTS = 8
    HISTORY_ENTRY_DOESNT_EXISTS = 9

    

    def message(self):
        ERROR_MESSAGES = [
            "Unknown error",

            "User already exists",
            "User does not exists",
            "Wrong password",

            "Ticket already exists",
            "Ticket does not exists",

            "Ticket type already exists",
            "Ticket type does not exists",

            "History entry already exists",
            "History entry does not exists"
            
        ]
        return ERROR_MESSAGES[self.value]

class ErrorCodeException(BaseException):
    def __init__(self,error_code):
        self.error_code = error_code

    def message(self):
        return self.error_code.message()

    