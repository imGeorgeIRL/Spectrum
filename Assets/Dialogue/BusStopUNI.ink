INCLUDE globals.ink 

{Day_Of_Week:
- 0: {isNight : ->Monday | ->MondayNight }
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}


===MondayNight===
Okay, first day didn't go too badly #speaker:Austin
could've gone better though
that noah guy seems
    *[interesting]
    *[friendly]
    *[..im not too sure yet actually]
    - where should I go now?
    *[Home?]
    ~busChosen(1)
        I think the bus is coming now...
        ->DONE
    *[Into town?]
    ~busChosen(2)
    
        - I think the bus is coming now...
        ->DONE

===Monday===
Where should I go?
        *[Home?]
    ~busChosen(1)
        I think the bus is coming now...
        ->DONE
    *[Into town?]
    ~busChosen(2)
    -I think the bus is coming now...
    ->DONE

===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
