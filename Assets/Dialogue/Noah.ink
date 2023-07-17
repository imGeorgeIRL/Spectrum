INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}



===Monday===
{noah_Interractions == 1: ->Noah2}
{noah_Interractions >= 2: ->Noah3}

Hey, what's up? #speaker:??? 
    *[Dr. Miller told me to ask if I can sit here,]
        is that okay? #speaker:Austin
        yeah! that's totally fine
        seat's all yours.
        ~sittingDown(1)
        ~noah_Interractions += 1
        ~spokenToNoah = true
 
    *[Can I sit here?]
        no... #speaker:??? 
        oh, okay, sorry #speaker:Austin
        dude, I was kidding#speaker:??? 
        of course you can sit here.
        ~sittingDown(1)
        ~noah_Interractions += 1
        ~spokenToNoah = true

    -...Thank you. #speaker:Austin        
      -> DONE

===Noah2===
What's your name? #speaker:???
~ dailyTasks(3)
Austin #speaker:Austin
... #speaker:???
Mine's Noah! #speaker:Noah
    *[Do you Noah guy?]
        ...#speaker:Noah
        ...
        ...
        <shake a=0.5> HAHAHAHAH </shake>
        <shake> HAHAHAHAHAHAHA </shake>
        That was a good one!!
        Thanks #speaker:Austin
        ~noah_Interractions += 1
        ~spokenToNoah = true
        
        ->DONE

===Noah3===
Cant wait to get started #speaker: Noah
~turnNight(1)
me too #speaker: Austin
~spokenToNoah = true

->DONE



===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE