# It's for commonly used responses

success = lambda : \
    { "message" : "Success" } 

unauthorized = lambda msg : \
    { "error" : msg }

error_code = lambda ec : \
    { "error" : ec.message() }