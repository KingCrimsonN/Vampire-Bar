VAR money = 0
VAR customer = 1
VAR choice1 = 0
VAR choice2 = 0
VAR choice3 = 0

THE BLOODIEST MARY
By invitation only.
All drinks $50. Tips are appreciated.

* [Start] -> d1_intro

=== d1_intro ===
VAR body1 = "A woman in her early twenties, passed away due to blunt  in an alley next to a health food store."
VAR body2 = "A teenage boy harvested from the local morgue. Acoording to the coroner's report, he died of acute respiratory distress after ingesting a laundry detergent pod."
VAR body3 = "Male, late thirties, retrieved from a local bar after starting a drunken fight and getting knocked out."
A new delivery has arrived.

DONOR 1:
{body1}

DONOR 2:
{body2}

DONOR 3:
{body3}

    * [OK] -> d1_1

=== d1_1 ===
YOU: Um, are you alright? You don't look too-

CHARLIE: I'm so sick, man. Is this... the right place?
    * [There is a hospital two blocks away.] -> d1_1_hospital
    * [What happened?] -> d1_1_story
    
=== d1_1_hospital ===
YOU: There is a hospital two blocks away. Dr. Nielsen works night shifts and sees patients with... blood-related issues.

CHARLIE: Yeah, that's a no-go, man. I'm trying to stay off the grid, if you know what I'm saying.

    * [Tell me what happened.] -> d1_1_story

=== d1_1_story ===
YOU: If you tell me what happened, perhaps I could help?

CHARLIE: I had a little... altercation. Nothing too bad, but now some people are looking for me, and I've been on the run for the past week. So earlier today I checked into this motel not far from here, right?

CHARLIE: I was tired, not really paying attention to my surroundings, and I crashed on this dingy couch, and what I didn't notice is there was a bible on the couch. I mean, who puts a fucking bible on a couch? 

CHARLIE: I got violently sick after this, and I'm just... not feeling well, you know. So I just came here because I'd heard of the place, I didn't know what else to do, and I figured I could use a drink.
    -> d1_choice

=== d1_choose_body ===
# backroom
{ 
    - customer == 1: 
    + [Body 1] -> d1_1_body1
    + [Body 2] -> d1_1_body2
    + [Body 3] -> d1_1_body3
 - customer == 2:
    + [Body 1] -> d1_2_body1
    + [Body 2] -> d1_2_body1
    + [Body 3] -> d1_2_body1
 - else:
    + [Body 1] -> d1_1_body1
    + [Body 2] -> d1_1_body2
    + [Body 3] -> d1_1_body3
}
    
=== d1_1_body1 ===
CHARLIE: Ooh, that hit the spot. I'm feeling better already. Thanks man!

YOU: You're welcome. You need some more good stuff, you come back.

CHARLIE: Nah, I don't think I'm sticking around. Need to get a move on, what with the fucking cops- I mean, nevermind. I gotta go now.

~ money += 75

    * [Next client] -> d1_2

=== d1_1_body2 ===
CHARLIE: Blaargh... what the hell is this stuff? I'm feeling even worse now. Are you trying to poison me? Fuck you man, I'm outta here.
    * [Next client] -> d1_2

=== d1_1_body3 ===
CHARLIE: This... is not- hic!- too bad. It's been a while since I had a proper drink. All because of those fucking coppers. You know what, I'm done with this bullshit! I'm not running anymore. I'm going to go and... settle the scores. 

~money += 50

    * [Next client] -> d1_2

=== d1_2 ===
~ customer = 2
YOU: Hi there! What can I do you for?

DIEGO: Heya, how's it going? A friend told me about this place. Can you hook me up with some good stuff? Like, something a bit... spicy, if you catch my drift?
    * [Spicy?] -> d1_2_what
    * [You came to the right place!] -> d1_2_yep

=== d1_2_what ===
YOU: Spicy? We don't do spices. We just focus on the... core product.

DIEGO: Man, I don't mean literally spicy. I want something with a kick, you know? Something... bad. Not bland and boring. -> d1_choice

=== d1_2_yep ===
YOU: You came to the right place! What's your poison?

DIEGO: Surprise me! Just make sure it's not bland and boring like the regular stuff. -> d1_choice

=== d1_choice ===

    * [I have just what you need.] -> d1_choose_body
    
=== d1_2_body1 ===
DIEGO: What the hell, man? I specifically asked for something spicy, and you're gonna give me the blandest freaking blood I've ever had? I'm not paying for this shit. Hasta la vista.
~money += 0
-> d1_3

=== d1_2_body2 ===
DIEGO: Ooh, this... is... fucking dope! That's what I'm talking about. I'm so glad I came here! I think I'm going to be sick... but this is awesome man, thanks a bunch!
~money += 75
-> d1_3

=== d1_2_body3 ===
DIEGO: This stuff... is kind of meh. I'm peeved, actually. I walked forty minutes to get here, and this is what I get?
DIEGO: I'll pay for the drink and everything, but you'll need to do better next time, dude. If I ever visit this place again, which is a big if.
~money += 50
-> d1_3

=== d1_3 ===
~ customer = 3
CAROLINE: ...

YOU: Hi there!

CAROLINE: ...

YOU: Welcome to the Bloodiest Mary! Anything I can do for you?

-> END  