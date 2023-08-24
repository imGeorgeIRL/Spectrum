INCLUDE globals.ink

{giftsBought:
- 0: ->Zero
- 1: ->One
- 2: ->Two
- 3: ->Three
}

===Zero===
Okay, I can buy three things #speaker: Austin
which shirt should I look at
    *Black holes?
        These are really soft!
        which colour??
            **white
            **blue
            **Red
    *solar system?
        These are really soft!
        which colour??
            **Orange
            **Grey
            **Green
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
        *Black holes?
        These are really soft!
        which colour??
            **white
            **blue
            **Red
    *solar system?
        These are really soft!
        which colour??
            **Orange
            **Grey
            **Green
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
        *Black holes?
        These are really soft!
        which colour??
            **white
            **blue
            **Red
    *solar system?
        These are really soft!
        which colour??
            **Orange
            **Grey
            **Green
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
