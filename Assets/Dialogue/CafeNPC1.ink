INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: ->Tuesday
- 2: -> Wednesday

}

===Monday===
{townNPC1 : ->Monday2 }
Hi sweatheart, can I  help you? #speaker:Woman
    * [Which bus do I need to catch to get to Cygnus University?]
    ~spokeTo(1)
        Oh im not sure honey,#speaker:Woman
        You might have to ask someone over there.
        ->DONE
    * [never mind...]
    Alright, suit yourself.#speaker:Woman
    ~townNPC1 = true
    ~town1 = 1
    ->DONE

===Monday2===
You might have to ask someone else #speaker:Woman
sorry sweetheart.
->DONE

===Tuesday===
{town1 == 0: ->Monday}
{townNPC1 : ->Tuesday2 | ->Tuesday1}

=Tuesday1
Oh it's you again!#speaker:Woman
Didn't find out which bus you needed?
I did, I just <fade d=1>forgot</fade> again #speaker:Austin
Oh no!#speaker:Woman
Unfortunately im just as clueless as yesterday
but I think the same fella is over there again!
Thank you #speaker:Austin
No problem.#speaker:Woman
~spokeTo(1)
~townNPC1 = true
~town1 = 2
->DONE

=Tuesday2
Hope you remember this time haha!#speaker:Woman
->DONE

===Wednesday===
{town1 == 0: ->Monday}
{town1 == 1: ->Tuesday}
Look at you, third time's the charm! #speaker:Woman
Lost again?
Nope! I'm supposed to be here this time. #speaker:Austin
Trying to find the clothing store
what is it called? Cloth and co?
Threads and co! #speaker:Woman
Yes that's the one! Do you know where it is? #speaker:Austin
Of course, it's just back the way you came from #speaker:Woman
past the bus stop, you can't miss it.
Thank you so much #speaker:Austin
no worries sweetheart #speaker:Woman
buy something nice for yourself.
~town1 = 3
->DONE



