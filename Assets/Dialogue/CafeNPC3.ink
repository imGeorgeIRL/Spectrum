INCLUDE globals.ink

{Day_Of_Week:
- 0: -> Monday
- 1: ->Tuesday
- 2: -> Wednesday
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
~town3 = 1
->DONE

===Monday2===
What do you want? #speaker:Guy
nothing, sorry #speaker:Austin
->DONE


//TUESDAY *****************************************
===Tuesday===
{town3 == 0: ->Monday}
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
~town3 = 2
->DONE

=Tuesday2
Please, stop bothering me.#speaker:Guy
->DONE


//WEDNESDAY ******************************************
===Wednesday===
{town3 == 0: ->Monday}
{town3 == 1: ->Tuesday}
Okay, I'll bite. #speaker:Guy
what is <shake a=0.5>what</shake> is so important
that you have to bug me every day this week
umm.. #speaker:Austin
    *To be honest, I dont know
        You just seemed, interactable
        Well, im not #speaker:Guy
        so stop trying to interact with me.
        ... #speaker:Austin
    *I mean, you look lonely
        So I thought i'd talk to you
        lonely? can a guy not just be by himself #speaker:Guy
        I suppose you're right #speaker:Austin
        I mean I am lonely, #speaker:Guy
        but im not looking to change that
        so...there!
        ... #speaker:Austin
    *You have something on your shirt #speaker:Austin
        I've been trying to tell you
        I think it's chocolate
        oh, thank you. #speaker:Guy
        Oh that's not chocolate
        <shake a=0.5>what ?</shake> #speaker:Austin
        its barbeque sauce! #speaker:Guy
        oh thank god #speaker:Austin
    - What's your name anyway? #speaker:Guy
    Austin. What's yours? #speaker:Austin
    Im not telling you that #speaker:Guy
    I just wanted to put a name to the face
    that's been bugging me every day.
    oh. #speaker:Austin
~town3 = 3
->DONE

