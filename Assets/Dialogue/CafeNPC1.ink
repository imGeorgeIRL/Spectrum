INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
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
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE

