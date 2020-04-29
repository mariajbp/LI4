# It's for commonly used responses

success = lambda x=None : \
    { "message" : "Success" if x == None else x } 

unauthorized = lambda msg : \
    { "error" : msg }

error_message = lambda msg : \
    { "error" : msg }

error_code = lambda ec : \
    { "error" : ec.message() }