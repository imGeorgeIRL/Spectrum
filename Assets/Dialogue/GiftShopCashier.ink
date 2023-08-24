INCLUDE globals.ink

{giftsBought == 0: ->preChosen | ->postChosen}

===preChosen===
did you need help finding anything? #speaker: Cashier
    *no thank you #speaker: Austin
        alright, let me know if that changes #speaker: Cashier
        ->DONE 
    *Yeah I had a question #speaker: Austin
        Do you like space,
        or are you just here for the money?
        excuse me? #speaker: Cashier
        like is this just a job, or is it a passion #speaker: Austin
        It's just a job, man #speaker: Cashier
        damn, unfortunate
        ->DONE
->DONE

===postChosen===
Did you find everything okay? #speaker: Cashier
    *yeah #speaker: Austin
    *i guess #speaker: Austin
- will that be cash or card? #speaker: Cashier
Card thanks #speaker: Austin
would you like a reciept with that? #speaker: Cashier
    *yes Please #speaker: Austin
    *no, thank you #speaker: Austin
-alright, Have a good one #speaker: Cashier
have a <dangle a=0.4>Stellar</dangle> rest of your day #speaker: Austin
...I dont get paid enough for this #speaker: Cashier
->DONE