INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}

===Monday===
Should I watch some Tv? #speaker: Austin
    *[Yeah]
    what should I watch?
        **[space documentary?]
        ~tvChoice(1)
            I think I have one on my watch list 
            its on <bounce a= 0.2>Black Holes</bounce>
            ~watchTv(1)
            ->DONE
        **[The news?]
        ~tvChoice(2)
            I dont think theres much going on
            but I could have a look I suppose.
            ~watchTv(1)
            ->DONE
        **[Reality Tv?]
        ~tvChoice(3)
            maybe I should check it out,
            see what all the fuss is about.
            ~watchTv(1)
            ->DONE
    *[Nah, im pretty tired]
        {hasEatenDinner: 
        I think I just want to go to bed.
        - else:
        I should eat some food, then go to bed.
        }
    ->DONE

    
===Tuesday===
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE