INCLUDE globals.ink

{Day_Of_Week:
- 0: {isNight : ->MondayNight | ->Monday }
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
    Actually, I think I just want to go home
    ~busChosen(1)
    
        - I think the bus is coming now...
        ->DONE

===Monday===
Where should I g- #speaker: Austin
wait,
I'm at uni already,
why would I go anywhere?
    ->DONE

===Tuesday===
{tuesdayUni : ->postUni | ->preUni}

    =preUni
    I shouldn't leave now that I'm here #speaker: Austin
    that would make no sense
    ->DONE

    =postUni
    Okay, just think #speaker: Austin
    think and breathe, you're okay Austin
    the meltdown is over, It's done
    Noah said to meet you at the cafe with-
    oh no,
    I've forgotten again!
    Why do I keep forgetting these things?!
    okay no, stop, breathe.
    just catch the bus into town
    you can figure it out from there.
    ~busChosen(2)
    ->DONE
    
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE
