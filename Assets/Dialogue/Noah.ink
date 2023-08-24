INCLUDE globals.ink

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
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
{deepConvoPause : ->Tuesday6 }
{deepConversation: ->Tuesday5 }
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
    dude take a seat, #speaker:Noah #size:small
    ~sittingDown(1)
    So...physics huh #speaker:Noah #size:small
    huh? I can't hear you over all this noise#speaker: Austin #size:normal
    what noise? It's pretty quiet in here#speaker:Noah #size:small
    I can't focus on anything you're saying#speaker: Austin #size:normal
    should I talk louder?#speaker:Noah #size:small
    No I think that would make it worse#speaker: Austin #size:normal
    I'm sorry, I gotta go!#speaker: Austin #size:normal
    o-okay, wait! #speaker:Noah #size:small
    ~deepConversation = true
    ->DONE
    
    =Tuesday5
        Hey! Are you okay? #speaker:Noah #size:normal
    No #speaker: Austin
        Everything just get too much in there? #speaker:Noah
    yes #speaker: Austin
    wanna talk about it? #speaker:Noah
    ... #speaker: Austin
    do you know what a meltdown is?
        No, not really #speaker:Noah
    you know that feeling #speaker: Austin
    when it feels like the whole world is collapsing
    right on top of you?
    like, it feels like you're standing
    on really thin ice,
    and someone is about to throw a rock on it.
        You've lost me #speaker:Noah
    Basically, its like #speaker: Austin
    the intensity of everything
    gets cranked up all the way
        right... #speaker:Noah
    so, sounds are super loud
    and I can't block anything out.
    Everything is brigher,
    smells are too intense,
    anything touching me feels like sandpaper
    or burning.
    It's all so overwhelming that I either
    freak out, or shut down.
        can you do anything to like, #speaker:Noah
        block anything out?
    I usually have noise cancelling headphones #speaker: Austin
    but they broke in the move here
        oh, that sucks, im sorry #speaker:Noah
        what do you wanna do now?
    Just lie here for a bit and calm down #speaker: Austin
        okay
    ~deepConvoBreak(1) //maybe add a sunset?? or just night
    ~deepConvoPause = true
    ->DONE
    
    =Tuesday6
        You like space right? #speaker:Noah
    yeah #speaker: Austin
        Obviously, #speaker:Noah
        why else would you be studying astrophysics
        and space science?
        anyway
        do you like black holes?
    //switch to austin sitting up
    Yes, they're my favourite thing ever! #speaker: Austin
        Me too! #speaker:Noah #size:big
        sorry, #size:normal
        Me too! #size:small
    I had a dream once when I was a kid #speaker: Austin #size:normal
    where I dove head first 
    right into a black hole
    and I distinctly remember
    being about to cross the event horizon,
    seeing this...infinite darkness ahead of me
    I woke up and just...knew
    that I had to do everything I could
    to know everything I could about the universe.
    I may not know people, but I know space.
        Me, personally, #speaker:Noah
        I think astrobiology is my calling.
        I want to be the guy that discovers new plant life
        on other planets, cataloguing them
        like the sci-fi Charles Darwin
        only cooler
        and more ginger!
    <shake> hahahaha! </shake> #speaker: Austin
        I like you Austin, #speaker:Noah
        I'm glad we're friends.
    friends? #speaker: Austin
        yeah, friends! #speaker:Noah
        ~timeSkip(1)
        ~isNight = true
    ->DONE
->DONE

===Wednesday===
{NoahWednesdayTown: ->Wednesday2}
=Wednesday1
    Oh hey! Fancy seeing you here! #speaker:Noah
    hey... #speaker: Austin
        *I'm sorry about yesterday #speaker: Austin
            nonsense, I totally get It #speaker:Noah
            I actually did some research last night about Autism
            just so I could understand a little better
                **no one has ever done that for me #speaker: Austin
                    Well, one person has now! #speaker:Noah
                **that's really cool of you! #speaker: Austin
                    its the least I could do #speaker:Noah
                    you're my friend and I wanna make you comfortable!
                -- that means a lot to me honestly #speaker: Austin
        *wait, you dont hate me? #speaker: Austin
            what? why would I hate you? #speaker:Noah
            because of yesterday, #speaker: Austin
            I kind of ruined lunch
            nonsense, you didnt ruin anything #speaker:Noah
            besides, they let me take it to go
        *How are you? #speaker: Austin
            I'm good, just you know, #speaker:Noah
            lollygagging, meandering, loitering
            the usual
            how about you? how you feeling from yesterday
            I mean, i slept for about 12 hours, #speaker: Austin
            and im a little tired still
            but other than that, im feeling okay
            that's good #speaker:Noah
        - what'cha got there? #speaker: Austin
        oh this? it's a book I just bought #speaker:Noah
        what book did you buy? #speaker: Austin
        It's called "Mysteries of the Universe" #speaker:Noah
        oooh! that sounds cool #speaker: Austin
        yeah! speaking of the universe #speaker:Noah
        are you free tonight? 
        yeah, why? #speaker: Austin
        we should go to the observatory #speaker:Noah
        they're doing an exhibition on planetary bodies! 
            *yes! #speaker: Austin
            *yes!!! #speaker: Austin
            *yes!!!!!! #speaker: Austin
        - Alright cool! I'll pick you up at 8, #speaker:Noah
        just send me your address. 
        Sure! #speaker: Austin
        ~NoahWednesdayTown = true
        ~timeSkip(1)
        ->DONE
    
    =Wednesday2
    Hey, I am <shake a=0.5>so</shake> sorry
    I'm running a little late!
    I thought I'd tell you just in case you were stressing
    *I was stressing
        I figured that you would be
        It's all good,
        thanks for letting me know!
    *I was definitely <shake a=0.5>not</shake> stressing
        Austin, You are bad at sarcasm
        yeah...I'm stressing 
        it's alright hahaha
    -Ok, I'm pulling up to your house now
    I'll see you in a second
    the door is unlocked so let yourself in
    aight
    ~noah_Interractions = 2
    ->DONE


->DONE

