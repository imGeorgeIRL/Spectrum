INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
}

===Monday===
{isNight : -> MondayNight }
{FoundBus : ->Found | ->NotFound }

=Found
I finally figured out what bus to catch #speaker:Austin
~makeChoice(2)
Looks like it's right around the corner.
    ->DONE 

=NotFound
I dont know what bus to catch #speaker:Austin
maybe I need to ask people?
I just know im gonna get super overstimulated
    ->DONE

=MondayNight
alright, it's pretty late, #speaker:Austin
i'm just gonna go home
~busChosen(1)
->DONE


===Tuesday===
{FoundBus: ->Found | ->NotFound }

    =Found
    838, 838, thats the bus I need to take #speaker:Austin
    ~makeChoice(2)
    Here is is, right on time.
    ->DONE

    =NotFound
    Oh great...wrong bus #speaker:Austin
    lets just cross 519 off the list.
    I Need to find out what bus I need to catch
    but talking to people just drains me.
    ->DONE

->DONE

===Wednesday===
I feel like there's things I need to do... #speaker:Austin
->DONE


