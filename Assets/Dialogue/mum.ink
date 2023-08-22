INCLUDE globals.ink

{Day_Of_Week:
- 1: -> Tuesday
- 2: -> Wednesday
}

===Tuesday===
hello? #speaker:Austin #font:other
hi sweetie, it's mum! #speaker:Mum #font:mum
hi mum #speaker:Austin #font:other
how are you settling in? #speaker:Mum #font:mum
I think im okay, trying to settle into a routine #speaker:Austin #font:other
but everything is just so different
it might take me a bit to get used to.
Make any friends yet? #speaker:Mum #font:mum
    *I made one #speaker:Austin #font:other
        I think
    *It's only been one day #speaker:Austin #font:other
        I think it's too soon to tell
    *no not yet #speaker:Austin #font:other
        I'm trying though
- that's alright, #speaker:Mum #font:mum
I'm sure you'll make friends in no time!
maybe #speaker:Austin #font:other
I'm trying to seem "normal" enough
but I dont know if it's working
Honey! You dont have to pretend to be normal #speaker:Mum #font:mum
normal is overrated anyway!
Thanks mum, #speaker:Austin #font:other
but I dont think everyone else got that memo
They will in no time!#speaker:Mum #font:mum
alright sweetheart, I have to go now 
I'm glad the changes aren't too hard on you
I love you
yeah you too #speaker:Austin #font:other
seeya! #speaker:Mum #font:mum
bye mum. #speaker:Austin #font:other
~talkedToMum(1)
->DONE

===Wednesday===
Hi mum #speaker:Austin #font:other
hey sweetheart, thought I'd check in on you #speaker:Mum #font:mum
see how your day was yesterday
    *It was awful #speaker:Austin #font:other
        ->awful
    *It was okay #speaker:Austin #font:other
        ->okay
    *It went well #speaker:Austin #font:other
        ->good

->DONE

=gottaGo
-Anyway, I need to head off now #speaker:Austin #font:other
I've gotta run some errands today
Oh look at my little grown up!! #speaker:Mum #font:mum
Bye sweetie
love you. #speaker:Austin #font:other
love you too!! #speaker:Mum #font:mum
Bye #speaker:Austin #font:other
~talkedToMum(1) 
->DONE

=awful
Oh goodness honey, what happened? #speaker:Mum #font:mum
        I thought I had uni, and went to class #speaker:Austin #font:other
        but it was cancelled
        which sent me into a meltdown
        Oh honey, was the rest of your day good at least? #speaker:Mum #font:mum
            *not at all #speaker:Austin #font:other
                I went to lunch with a
                **friend
                **person I had met
                --and it was so loud that I freaked out again
                so I dont think they're gonna want to know me after that
                I'm sure they'll understand #speaker:Mum #font:mum
                just explain to them what happened
                and if they dont
                then you shouldn't want to be their friend anyway!
                Thanks mum, i'll try it. #speaker:Austin #font:other
                ->gottaGo
            *yeah, it was alright #speaker:Austin #font:other
                I went to lunch with a
                **friend
                **person I had met
                -- and it was pretty loud, which kinda sucked
                but we talked and yeah,
                the rest of the day was fine
                I guess...
                I'm glad, you're still getting used to things #speaker:Mum #font:mum
                you have to give yourself some leeway
                I know, im trying hard #speaker:Austin #font:other
                  ->gottaGo    
                  
=okay
Just okay? Are you sure? #speaker:Mum #font:mum
        *Yeah, had a rough start to the day #speaker:Austin #font:other
            but it got a little better towards the end
        *I mean... #speaker:Austin #font:other
        "okay" might be an overstatement
        I dont really want to get into it though
        I just want to put it past me and carry on
        - alright honey, #speaker:Mum #font:mum
        well at least you're feeling a litle better now right?
        yeah, a little better. #speaker:Austin #font:other
->gottaGo

=good
oh? do tell! #speaker:Mum #font:mum
        Well, I caught up with my friend at uni #speaker:Austin #font:other
        and we went and got lunch
        I got a {drink} which was nice
        and we just kind of talked
        Oh im glad you're making friends! #speaker:Mum #font:mum
        "friends" is an overstatement #speaker:Austin #font:other
        its just one guy.
        You should put yourself out there more often #speaker:Mum #font:mum
        It's hard mum, but im trying #speaker:Austin #font:other
->gottaGo