INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: {cafeOnMonday : ->Tuesday | -> Monday }
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
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
    ->DONE

===Monday2===
You might have to ask someone else #speaker:Woman
sorry sweetheart.
->DONE

===Tuesday===
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
->DONE

=Tuesday2
Hope you remember this time haha!#speaker:Woman
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE

