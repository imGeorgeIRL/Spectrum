INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
{isNight : -> MondayNight }
{FoundBus : ->Found | ->NotFound }

=Found
I finally figured out what bus to catch #speaker:Austin
~makeChoice(1)
Looks like it's right around the corner.
    ->DONE 

=NotFound
I dont know what bus to catch #speaker:Austin
maybe I need to ask people?
I just know im gonna get super overstimulated
    ->DONE

=MondayNight
Where should I go?
        *[Home?]
    ~busChosen(1)
        I think the bus is coming now...
        ->DONE
    *[Into Uni?]
    ~busChosen(2)
    -I think the bus is coming now...
    ->DONE
    ->DONE
->DONE


===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE

