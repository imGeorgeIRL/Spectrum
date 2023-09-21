INCLUDE globals.ink

{town3:
- 0: ->Monday
- 1: {townNPC3: ->Monday2 | -> Tuesday}
- 2: {townNPC3: ->Tuesday2 | -> Wednesday}
- 3: ->Wednesday2
}


//MONDAY ***************************************
===Monday===
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
What do you want? #speaker:Rude Guy
nothing, sorry #speaker:Austin
->DONE


//TUESDAY *****************************************
===Tuesday===
Oh god not you again, #speaker:Rude Guy
what do you want this time?
do you know which bus I have to catch? #speaker:Austin
I'm trying to get to uni
No, I dont, now go bother someone else,#speaker:Rude Guy
<shake a=0.5>Anyone else!</shake>
~spokeTo(1)
~townNPC3 = true
~town3 = 2
->DONE

===Tuesday2===
Please, stop bothering me.#speaker:Rude Guy
->DONE


//WEDNESDAY ******************************************
===Wednesday===
Okay, I'll bite. #speaker:Rude Guy
what is <shake a=0.5>what</shake> is so important
that you have to bug me every day this week
umm.. #speaker:Austin
    *To be honest, I dont know
        You just seemed, interactable
        Well, im not #speaker:Rude Guy
        so stop trying to interact with me.
        ... #speaker:Austin
    *I mean, you look lonely
        So I thought i'd talk to you
        lonely? can a guy not just be by himself #speaker:Rude Guy
        I suppose you're right #speaker:Austin
        I mean I am lonely, #speaker:Rude Guy
        but im not looking to change that
        so...there!
        ... #speaker:Austin
    *You have something on your shirt #speaker:Austin
        I've been trying to tell you
        I think it's chocolate
        oh, thank you. #speaker:Rude Guy
        Oh that's not chocolate
        <shake a=0.5>what ?</shake> #speaker:Austin
        its barbeque sauce! #speaker:Rude Guy
        oh thank god #speaker:Austin
    - What's your name anyway? #speaker:Rude Guy
    Austin. What's yours? #speaker:Austin
    Im not telling you that #speaker:Rude Guy
    I just wanted to put a name to the face
    that's been bugging me every day.
    oh. #speaker:Austin
~townNPC3 = true
~town3 = 3
->DONE

===Wednesday2===
Mate... #speaker:Rude Guy
can I not just enjoy my morning in peace
<shake a=0.5>please?!</shake>
->DONE
