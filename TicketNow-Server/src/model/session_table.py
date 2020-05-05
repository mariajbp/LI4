import datetime

class SessionTable:
    __MAX_SESSIONS_PER_USER = 10
    
    __authentication_table = dict({})

    @staticmethod
    def add(id_user,token_id,exp_date):
        
        if not id_user in SessionTable.__authentication_table:
            SessionTable.__authentication_table[id_user] = { token_id : exp_date }
            return 1
        else:
            session_tokens = SessionTable.__authentication_table[id_user]
            if len(session_tokens) < SessionTable.__MAX_SESSIONS_PER_USER:
                session_tokens[token_id] = exp_date
                return 1
            else:
                now = datetime.datetime.now()
                for tok in list(session_tokens):
                    if session_tokens[tok] < now:
                        del session_tokens[tok]
                if len(session_tokens) < SessionTable.__MAX_SESSIONS_PER_USER:
                    session_tokens[token_id] = exp_date
                    return 1
        # Returns 1 if the token was added, 0 otherwise
        return 0
    
    @staticmethod
    def remove(id_user,token_id):
        try:
            session_tokens = SessionTable.__authentication_table[id_user]

            del session_tokens[token_id]
            return 1
        except KeyError:    
            return 0

    @staticmethod
    def contains(id_user,token_id):
        try:
            SessionTable.__authentication_table[id_user][token_id]
            return 1
        except:
            return 0

    @staticmethod
    def size(id_user):
        try:
            return len(SessionTable.__authentication_table[id_user])
        except:
            return 0