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
{orderedDrink: ->Tuesday4 }
{noahInCafe: ->Tuesday3 }
{spokenToNoah : ->Tuesday2 | ->Tuesday1 }

=Tuesday1
    Woah, it's super dark in here #speaker: Noah
    where is everyone?
    ...
    are you okay?
    ...
    Austin?
        <shake>no, no no nO NO!</shake> #speaker: Austin
        <shake>Why is everything going wrong today?!</shake>
        <shake>why did nobody tell me class was cancelled?!</shake>
    huh? Class was cancelled? #speaker: Noah
        <shake>I didn't know, I didn't know, I did-</shake> #speaker: Austin
    It's okay! #speaker: Noah
        <shake>No it's not!</shake> #speaker: Austin #size: big
        <shake>I- I dont handle changes, at all</shake> #size: normal
        <shake>and this is just too much</shake>
    can I...do anything? to help? #speaker: Noah
        I think I just need to calm down a bit #speaker: Austin
        ~calmDown(1)
        ~spokenToNoah = true
    ->DONE

    =Tuesday2
    {tuesdayUni : ->Tuesday3}
    Hey, how you feeling?#speaker: Noah
        *I dont know#speaker: Austin
            It's really hard to just 
            I dont know,
            know? explain how im feeling?
        *drained#speaker: Austin
            That took a lot out of me
            like emotionally
            and physically I guess
        *I'm okay#speaker: Austin
            I just, dont handle change well
            I have this plan in my head
            and whenever something doesn't stick to it
            I freak out
    - That's alright mate, I get it#speaker: Noah
    I'm sorry you had to witness That#speaker: Austin
    no no, don't apologise, it's fine!#speaker: Noah
    Do you wanna grab some lunch?
        *yeah sure#speaker: Austin
            I should probably eat something
        *okay#speaker: Austin
            This is usually when I would eat lunch
        *I dont know#speaker: Austin
            I'm feeling kinda tired
                come on mate, you gotta eat something#speaker: Noah
            alright, fine#speaker: Austin
    - Awesome!#speaker: Noah
    There's this cafe in town I like to go to
    I can't remember the name of it, 
    but it has a green shade out front
    I gotta run a few errands, but meet me there for lunch!
        Alright, sure#speaker: Austin
    Seeya then buddy!#speaker: Noah
    ~tuesdayUni = true
    ~noahWalkAway(1)
    ~noahInCafe = true
    ->DONE

    =Tuesday3
    Hey! You made it! #speaker:Noah #size:small
    hey, It's really loud in here #speaker: Austn #size:normal
    yeah, sorry, it's usually not this busy #speaker:Noah #size:small
    but I guess with uni being back and all
    anyway
    you can go ahead and order, i've just ordered my drink
    alright #speaker: Austn #size:normal
    ~spokenToNoah = true
    ->DONE
    
    =Tuesday4
    what did you get?#speaker:Noah #size:small
    I got a {size} {drink} #speaker: Austin #size:normal
    oh yum, I got a caramel latte#speaker:Noah #size:small
    dude take a seat, #speaker: Austin #size:normal
    ~sittingDown(1)
    So...physics huh 
    huh? I can't hear you over all this noise#speaker: Austin #size:normal
    what noise? It's pretty quiet in here#speaker:Noah #size:small
    I can't focus on anything you're saying#speaker: Austin #size:normal
    should I talk louder?#speaker:Noah #size:small
    No I think that would make it worse#speaker: Austin #size:normal
    I'm sorry, I gotta go!#speaker: Austin #size:normal
    o-okay, wait! #speaker:Noah #size:small
    ->DONE
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE