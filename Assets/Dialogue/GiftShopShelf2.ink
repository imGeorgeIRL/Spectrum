INCLUDE globals.ink

{giftsBought:
- 0: ->Zero
- 1: ->One
- 2: ->Two
- 3: ->Three
}

===Zero===
Okay, I can buy three things #speaker: Austin
What should I look at first?
    *Hats
        They've got a really cool planet on the front!
    *plushies
        Which one?
            **Black hole
                Absolutely I am buying this for myself
                No doubt about it
                ~giftsBought = 1
                ->DONE
            **Astronaut
                Look at him go!
            **Rocketship
                Wonder where it's headed?
    -should I buy it?
        *yes
            Should I give it to Noah?
                **yeah
                    I think he'll love this
                    ~boughtForNoah = true
                    ~giftsBought = 1
                    ->DONE
                **nah, ill keep it
                    I'm sure i'll find something better for him
                    ~giftsBought = 1
                    ->DONE
        *no
            I'll keep looking
            ->DONE
->DONE

===One===
Two things left, #speaker: Austin
what to buy?
        *Hats
        They've got a really cool planet on the front!
    *plushies
        Which one?
            **Black hole
                Absolutely I am buying this for myself
                No doubt about it
                ~giftsBought = 2
                ->DONE
            **Astronaut
                Look at him go!
            **Rocketship
                Wonder where it's headed?
    -should I buy it?
        *yes
            Should I give it to Noah?
                **yeah
                    I think he'll love this
                    ~boughtForNoah = true
                    ~giftsBought = 2
                    ->DONE
                **nah, ill keep it
                    I'm sure i'll find something better for him
                    ~giftsBought = 2
                    ->DONE
        *no
            I'll keep looking
            ->DONE
->DONE

===Two===
Last item, better make it good, #speaker: Austin
What do they have?
        *Hats
        They've got a really cool planet on the front!
    *plushies
        Which one?
            **Black hole
                Absolutely I am buying this for myself
                No doubt about it
                ~giftsBought = 3
                ->DONE
            **Astronaut
                Look at him go!
            **Rocketship
                Wonder where it's headed?
    -should I buy it?
        *yes
            Should I give it to Noah?
                **yeah
                    I think he'll love this
                    ~boughtForNoah = true
                    ~giftsBought = 3
                    ->DONE
                **nah, ill keep it
                    I'm sure i'll find something better for him
                    ~giftsBought = 3
                    ->DONE
        *no
            I'll keep looking
            ->DONE
->DONE

===Three===
I already picked up three items #speaker: Austin
I should check out at the counter
->DONE
