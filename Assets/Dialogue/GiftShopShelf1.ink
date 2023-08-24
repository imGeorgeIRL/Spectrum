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
    *Mugs
        These look cool,
        they have a similar design to my shirt
    *Books
        There's a couple on this shelf
            **Stars, and why they matter
                This one is by "Dr. Akle Clive"
                sounds like a cool guy
            **Mysteries of the universe
                its by "brainy gizato"
                that sounds like an anagram...
    *Travel mugs
        I dont really travel anywhere though...
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
            ->Zero
->DONE

===One===
Two things left, #speaker: Austin
what to buy?
    *Mugs
        These look cool,
        they have a similar design to my shirt
    *Books
        There's a couple on this shelf
            **Stars, and why they matter
                This one is by "Dr. Akle Clive"
                sounds like a cool guy
            **Mysteries of the universe
                its by "brainy gizato"
                that sounds like an anagram...
    *Travel mugs
        I dont really travel anywhere though...
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
            ->Zero
->DONE

===Two===
Last item, better make it good, #speaker: Austin
What do they have?
    *Mugs
        These look cool,
        they have a similar design to my shirt
    *Books
        There's a couple on this shelf
            **Stars, and why they matter
                This one is by "Dr. Akle Clive"
                sounds like a cool guy
            **Mysteries of the universe
                its by "brainy gizato"
                that sounds like an anagram...
    *Travel mugs
        I dont really travel anywhere though...
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
            ->Zero
->DONE

===Three===
I already picked up three items #speaker: Austin
I should check out at the counter
->DONE
