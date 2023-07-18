INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
Should I watch some Tv?
    *[Yeah]
    what should I watch?
        **[space documentary?]
        ~makeChoice(1)
            I think I have one on my watch list 
            its on <bounce a= 0.2>Black Holes</bounce>
            ->watching
        **[The news?]
        ~makeChoice(2)
            I dont think theres much going on
            but I could have a look I suppose.
            ->watching
        **[Reality Tv?]
        ~makeChoice(3)
            maybe I should check it out,
            see what all the fuss is about.
            ->watching
    *[Nah, im pretty tired]
        {hasEatenDinner: 
        I think I just want to go to bed.
        - else:
        I should eat some food, then go to bed.
        }
    ->DONE

=watching
    I just have to find my remote
    ~watchTv(1)
    where did I leave it last?
    ->DONE
    
===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE