INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}



===Monday===
//{noah_Interractions == 1: ->Noah2}
//{noah_Interractions >= 2: ->Noah3}

Hey, what's up? #speaker:??? 
    *[Dr. Miller told me to sit here,]
        is that okay? #speaker:Austin
        Nope! This seat is reserved!#speaker:??? 
        Oh, sorry! #speaker:Austin
        Reserved for you of course!#speaker:??? 
        haha 
        ha
        ...
        yeah you can totally sit there,
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

    -Thank you. #speaker:Austin
No problem,#speaker:???
did you really think I was serious?
<pend>...yeah, I dont really understand sarcasm</pend>#speaker:Austin
are you autistic?#speaker:???
yeah#speaker:Austin
sweet! #speaker:???
What's your name? 
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
    *[cool]
        nice to meet you #speaker:Noah
        yeah, you too #speaker: Austin
        ~noah_Interractions += 1
        ~spokenToNoah = true
-Cant wait to get started #speaker: Noah
~turnNight(1)
~isNight = true
~timeSkip(1)
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