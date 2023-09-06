INCLUDE globals.ink 

{Day_Of_Week:
- 0: ->Monday
- 1: -> Tuesday
}

===Monday===
Should I watch some Tv? #speaker: Austin
    *[Yeah]
    what should I watch?
        **[space documentary?]
        ~tvChoice(1)
            I think I have one on my watch list
              ~watchTv(1)          
            its on <bounce a= 0.2>Black Holes</bounce>
            ->DONE
        **[The news?]
        ~tvChoice(2)
            I dont think theres much going on
            ~watchTv(1)            
            but I could have a look I suppose.
            ->DONE
        **[Reality Tv?]
        ~tvChoice(3)
            maybe I should check it out,
            ~watchTv(1)            
            see what all the fuss is about.
            ->DONE
    *[Nah, im pretty tired]
        {hasEatenDinner: 
        I think I just want to go to bed.
        - else:
        I should eat some food, then go to bed.
        }
    ->DONE

    
===Tuesday===
Should I watch some- #speaker: Austin
no, just sleep.
It's just gonna make me feel worse.
->DONE
