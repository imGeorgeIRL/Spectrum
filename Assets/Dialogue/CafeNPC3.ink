INCLUDE globals.ink

{Day_Of_Week:
- 0: -> Monday
- 1: {cafeOnMonday : ->Tuesday | -> Monday }
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}


//MONDAY ***************************************
===Monday===
{townNPC3 : ->Monday2 }
what? #speaker:Guy
uh... #speaker:Austin
spit it out dude. #speaker:Guy
sorry #speaker:Austin
~spokeTo(1)
~townNPC3 = true
bye
->DONE

===Monday2===
What do you want? #speaker:Guy
nothing, sorry #speaker:Austin
->DONE


//TUESDAY *****************************************
===Tuesday===
{townNPC3: ->Tuesday2 | ->Tuesday1}

=Tuesday1
Oh god not you again, #speaker:Guy
what do you want this time?
do you know which bus I have to catch? #speaker:Austin
I'm trying to get to uni
No, I dont, now go bother someone else,#speaker:Guy
<shake a=0.5>Anyone else!</shake>
~spokeTo(1)
~townNPC3 = true
->DONE

=Tuesday2
Please, stop bothering me.#speaker:Guy
->DONE


//WEDNESDAY ******************************************
===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE