VAR money = 0
VAR oldmoney = 0
VAR customer = 1
VAR choice1 = 0
VAR choice2 = 0
VAR choice3 = 0
VAR choiceY1 = 0
VAR day = 1

# black

THE BLOODIEST MARY
By invitation only.
All drinks $50. Tips are appreciated.

* [Start] -> d1_intro

=== d1_intro ===
~ day = 1

VAR body1 = "A young woman wearing running clothes. Body found in an alley next to a health food store. Cause of death: blunt force injury to the head."
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
# bar
# char Charlie
~ customer = 1

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
- day == 1:
    { 
    - customer == 1: 
    + [Body 1] -> d1_1_body1
    + [Body 2] -> d1_1_body2
    + [Body 3] -> d1_1_body3
 - customer == 2:
    + [Body 1] -> d1_2_body1
    + [Body 2] -> d1_2_body2
    + [Body 3] -> d1_2_body3
 - customer == 3:
    + [Body 1] -> d1_3_body1
    + [Body 2] -> d1_3_body2
    + [Body 3] -> d1_3_body3
 - customer == 100:
    + [Body 1] -> d1_Y_body1
    + [Body 2] -> d1_Y_body2
    + [Body 3] -> d1_Y_body3
    }
- day == 2:
    { 
    - customer == 1: 
    + [Body 1] -> d2_1_body1
    + [Body 2] -> d2_1_body2
    + [Body 3] -> d2_1_body3
    }
}
    
=== d1_1_body1 ===
~choice1=1
CHARLIE: Ooh, that hit the spot. I'm feeling better already. Thanks man!

YOU: You're welcome. You need some more good stuff, you come back.

CHARLIE: Nah, I don't think I'm sticking around. Need to get a move on, what with the fucking cops- I mean, nevermind. I gotta go now.

~ money += 75

    * [Next client] -> d1_2

=== d1_1_body2 ===
~choice1=2
CHARLIE: Blaargh... what the hell is this stuff? I'm feeling even worse now. Are you trying to poison me? Fuck you man, I'm outta here.
    * [Next client] -> d1_2

=== d1_1_body3 ===
~choice1=3
CHARLIE: This... is not- hic!- too bad. It's been a while since I had a proper drink. All because of those fucking coppers. You know what, I'm done with this bullshit! I'm not running anymore. I'm going to go and... settle the scores. 

~money += 50

    * [Next client] -> d1_2

=== d1_2 ===

~ customer = 2
# char Diego

YOU: Hi there! What can I do you for?

DIEGO: Heya, how's it going? A friend told me about this place. Can you hook me up with some good stuff? Like, something a bit... spicy, if you catch my drift?
    * [Spicy?] -> d1_2_what
    * [You came to the right place!] -> d1_2_yep

=== d1_2_what ===
YOU: Spicy? All of our product is... flavorful. But we don't do spices per se.

DIEGO: Man, I don't mean literally spicy. I want something with a kick, you know? Something... bad. Not bland and boring. 
-> d1_choose_body

=== d1_2_yep ===
YOU: You came to the right place! What's your poison?

DIEGO: Surprise me! Just make sure it's not bland and boring like the regular stuff. -> d1_choice

=== d1_choice ===

    + [I have just what you need.] -> d1_choose_body
    
=== d1_2_body1 ===
~choice2=1
DIEGO: What the hell, man? I specifically asked for something spicy, and you're gonna give me the blandest freaking blood I've ever had? I'm not paying for this shit. Hasta la vista.
~money += 0
    * [Next client] -> d1_3

=== d1_2_body2 ===
~choice2=2
DIEGO: Ooh, this... is... fucking dope! That's what I'm talking about. I'm so glad I came here! I think I'm going to be sick... but this is awesome man, thanks a bunch!
~money += 75
    * [Next client] -> d1_3


=== d1_2_body3 ===
~choice2=3
DIEGO: This stuff... is kind of meh. I'm peeved, actually. I walked forty minutes to get here, and this is what I get?
DIEGO: I'll pay for the drink and everything, but you'll need to do better next time, dude. If I ever visit this place again, which is a big if.
~money += 50
    * [Next client] -> d1_3

=== d1_3 ===

~ customer = 3
# char Caroline

CAROLINE: ...

YOU: Hi there!

CAROLINE: ...

YOU: Welcome to the Bloodiest Mary! Anything I can do for you?

CAROLINE: Uh... Sorrythiswasprobablyabadidea.

YOU: Um, what was that again? I didn't catch it.

CAROLINE: I just... I need a drink. I think. I normally go to the hospital for this stuff, but today... It's been such a- I don't know.

    * [Hospital blood can be a bit... sterile.] -> d1_3_hospital
    * [You seem agitated. Can you tell me what's bothering you?] -> d1_3_agitated
    
=== d1_3_hospital ===
YOU: I know what you mean. Dr. Nielsen offers quality blood, but it's very sterile and neutral. When you want a drink that makes you feel something, you come to the Bloodiest Mary.

CAROLINE: I just... want this to go away, you know?
-> d1_3_story

=== d1_3_agitated ===
YOU: You look agitated. Can you tell me what's bothering you? Perhaps I could help.
-> d1_3_story

=== d1_3_story ===
CAROLINE: It's... my boss. He really, really sucks! He's always pushing me around. Calling on me to work on my days off. Demanding I do stuff he knows I'm bad at, just to mock me afterwards.

CAROLINE: Even when I do well, he just dismisses it as if the work got done by itself. He's so mean all the time.

CAROLINE: I've been meaning to confront him. Tell him to stop. If he hates me so much, he can fire me. I just don't want to continue like this.

CAROLINE: But I... just don't have the courage, you know? I open my mouth, and no sound comes out. And he just mocks me again.

CAROLINE: It's so... pathetic. I'm a vampire. I'm supposedly five times stronger than this guy. And I can't get him to stop bullying me.

YOU: I'm sorry you're going through this. Your boss sounds like an absolute asshole. But there's good news.

-> d1_choice
    
=== d1_3_body1 ===
~choice3 = 1
~money += 50
CAROLINE: It's... T-this was a bad idea. I shouldn't have come here. I’ve got work to finish, and he may be coming by to check on my progress anytime soon...

CAROLINE: Here's your money. I’m sorry for wasting your time!

YOU: You didn't... oh well.
* [Next client] -> d1_4

=== d1_3_body2 ===
~choice3 = 2
CAROLINE: This tastes... horrible! Is this... ghh... some kind of prank? You're just like him! Why are you all like- blargh!
* [Next client] -> d1_4

=== d1_3_body3 ===
~choice3 = 3
~money += 75
CAROLINE: You know... you know what? I can't take this anymore. I'm so sick of him. I'm not some pushover, you know. If he thinks he can treat me like trash, he's got another thing coming!

YOU: Figured a little liquid courage was what you needed. Just be careful, okay?

CAROLINE: He's the one that needs to be careful! I'm going to go and put an end to this right now. And thank you, this was a great drink!
* [Next client] -> d1_4

=== d1_4 ===

~customer = 100
# char Yuuka

YUUKA: Hello.

YOU: Yuuka, good to see you!

YUUKA: Yeah, yeah. How are you doing?
    * [Can't complain. Business as usual.] -> d1_4_usual
    * [It's been a weird day.] -> d1_4_weird

=== d1_4_usual ===
YOU: Can't complain. Business as usual, nothing out of the ordinary.

YUUKA: Funny how after a while, harvesting corpses and dealing blood to vampires becomes "nothing out of the ordinary," huh?

YOU: When you put it like that...
->d1_4_continue

=== d1_4_weird ===
YOU: It's been a weird day. Weird donors. Weird patrons.

YUUKA: I guess you get all kinds in this line of work.

YOU: You sure do.
->d1_4_continue

=== d1_4_continue ===
YUUKA: It's fucking weird, isn't it? What you do? What I do? And how we don't even give a fuck?

YOU: I suppose. Are you still not...

YUUKA: Feeling anything? Nope. Not a goddamn thing. Not until I get your stuff, and even then it's only for a short while.

YUUKA: I'm not even angry at myself for not feeling anything. Or sad. Not emotionally, at least. I'm just... empty inside.

YOU: Let's fix that. What do you feel like tonight?

YUUKA: Killing myself? Nah, just kidding. I couldn't be bothered. I'm up for whatever you give me, old man.

-> d1_choice

=== d1_Y_body1 === 
~choiceY1 = 1
YUUKA: Hm. This one is kind of bland, if I'm being honest. It's kind of like Nielsen's stuff. Like vegetable salad with no dressing or herbs. Man, I miss actual vegetable salad.

YOU: Sorry it wasn't to your liking.

YUUKA: I did say I was up for whatever you'd give me. How much is it?
-> d1_4_leave

=== d1_Y_body2 ===
~ choiceY1 = 2 
YUUKA: What the fuck? I mean, I did say I'd have whatever you give me, but this is disgusting, man. Like, I'm not exaggerating, this stuff is making me sick.

YUUKA: My friendly advice is stop doing business with whoever harvested this body for you.

YOU: I'll... look into this. Sorry! I can get you another drink from a different donor.

YUUKA: No need. I did get my fix anyway, and I try not to overdo the whole blood drinking thing. How much is this vile fucking stuff? 
->d1_4_leave 

=== d1_Y_body3 ===
~choiceY1 = 3
YUUKA: This feels... angry. It's good, though. I wanted to be angry at myself for a long time. Now I am. A bit. For a while.

YOU: Just don't do anything reckless, okay?

YUUKA: As if I would. Might break a thing or two, but nothing more. Not your stuff, don't worry. How much do I owe you?
->d1_4_leave 

=== d1_4_leave ===
YOU: It's on the house, as usual.

YUUKA: Thanks old man. I better get going now. See ya!

YOU: Take care!

->d1_end

=== d1_end ===
# black
Tonight, you earned {money} dollars. <>
{
    -money <= 125:
    At this rate, you don't know if you will be able to pay rent at the end of the month.
    -money > 125 and money <= 175:
    You may just be able to pay rent at the end of the month.
    -else:
    Business is going well.
}

One of your customers <>
{
    -choice1 == 1:
    has recovered from a malaise thanks to the wonders of healthy nutrition.
    -choice1 == 2:
    thinks you tried to poison him.
    -choice1 == 3:
    is about to kill a police officer in a fit of rage.
}

Another customer <>
{
    -choice2 == 1:
    will look for other places to get his fix.
    -choice2 == 2:
    is high on poisoned blood and ecstatic about it.
    -choice2 == 3:
    might just pick a fight with a random passerby.
}

Yet another customer <>
{
    -choice3 == 1:
    is still too scared to confront her abusive boss.
    -choice3 == 2:
    is crying and vomiting in an office bathroom. 
    -choice3 == 3:
    has finally mustered the courage to confront her abusive boss.
}

And an old friend of yours <>
{
    -choiceY1 == 1:
    is going home disappointed.
    -choiceY1 == 2:
    will go home and try to sleep off her sickness.
    -choiceY1 == 3:
    punched a shop window, shattering it into tiny bits, and felt a little bit alive.
}

+ [Next day] -> d2_intro

=== d2_intro ===
~ day = 2
~ body1 = "Female, late twenties, had a heart attack in a nightclub after overdosing on MDMA."
~ body2 = "A man in his forties, kidnapped and stabbed to death following a money dispute."
~ body3 = "Male, mid-thirties, taken off life support after spending several months in a coma."
A new delivery has arrived.

DONOR 1:
{body1}

DONOR 2:
{body2}

DONOR 3:
{body3}

    * [OK] -> d2_1
    
=== d2_1 ===
# bar
# char Timotei
~ customer = 1

TIMOTEI: Ooh, it's so good to sit down. I'm so tired.

    + [Long night?] -> d2_1_night
    + [Anything I can get you?] -> d2_1_drink
    
=== d2_1_night ===
YOU: Long night?

TIMOTEI: Every night is damn long when you work in the docks. We get new shipments each night, and all those crates and containers need unloading.

TIMOTEI: Let me tell you, I'm a vampire and I can barely feel my arms after handling those goddman crates for hours on end. I have no idea how the humans are managing to survive in this job.

TIMOTEI: I have to go back there and finish my work, but I'm so exhausted already.
-> d1_choice

=== d2_1_drink ===
YOU: Anyting I can get you?

TIMOTEI: I sure hope so. I could use a boost after handling those goddman crates for hours on end. And I have another shipment to unload tonight.

-> d1_choice

=== d2_1_body1 ===
TIMOTEI: Wow, that's something else! I feel energized. I figure I can handle those crates quite easily now. Thanks pal, much appreciated!

TIMOTEI: I don't earn enough to tip, but maybe I can tip you off instead. Someone in the docks has been asking around about you. This is how I learned about this place, actually.

TIMOTEI: The guy goes by Richard, although who knows if it's his real name. He works for Germane Cargo, which owns a small warehouse in the docks, although I've never seen any actual cargo go in or out.

TIMOTEI: Anyway, I have to go now. Good luck!

~money += 50
* [Next client] -> d2_2

=== d2_1_body2 ===
TIMOTEI: Oh god, what is this? It tastes strange. Did you spike my drink? Are you trying to kill me?

YOU: I assure you I have no intention-

TIMOTEI: What possessed me to come here in the first place? I'm in deep shit if they discover I've been slacking off. What an idiot, what was I thinking?
* [Next client] -> d2_2

=== d2_1_body3 ===
TIMOTEI: Mm. Strange stuff, this. I... feel so tired now. I just want to lie down and have a nap. Here, take the money. And I need... rest.

~money += 50

* [Next client] -> d2_2

=== d2_2 ===

-> END  