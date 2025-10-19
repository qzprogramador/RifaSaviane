from pixqrcodegen import Payload

# Configuração básica
pix = Payload(
   "Andrey Costa de Queiroz",
  "02459626207",
 "500.00", 
 "MANAUS",
  "6909009062345"  
)

pix.gerarPayload()
