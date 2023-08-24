INCLUDE globals.ink

{clothesChosen == 0: ->preChosen | ->postChosen}
===preChosen===
Hi there, can I help you with anything? #speaker: Cashier
    *no thank you #speaker: Austin
        alright, #speaker: Cashier
    *is there a sale on? #speaker: Austin
        what? are you serious? #speaker: Cashier
        no, im just joking #speaker: Austin
        those signs are huge
        yeah... #speaker: Cashier
- let me know if you need any more help #speaker: Cashier
->DONE

===postChosen===
Did you find everything okay? #speaker: Cashier
    *yeah #speaker: Austin
    *i guess #speaker: Austin
- Your total comes to $ {costOfClothes} #speaker: Cashier
would you like a reciept with that?
    *yes Please #speaker: Austin
    *no, thank you #speaker: Austin
-alright, have a great rest of your day #speaker: Cashier
you too #speaker: Austin
->DONE