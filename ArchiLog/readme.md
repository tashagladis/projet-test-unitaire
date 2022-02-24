
**Rapport des tests unitaires**
========

**0- L'outil utilisé pour les tests**
=====

xUnit.net est un outil de test unitaire gratuit, open source et axé sur la communauté pour le .NET

![image](https://user-images.githubusercontent.com/69635011/155514561-e63ce7b2-bc67-42f4-8a8d-43e1451b458e.png)

**1- Le nombre de tests passés :**
=====

![image](https://user-images.githubusercontent.com/69635011/155510292-cbda5972-958a-45a4-962a-c00df4d8c50a.png)

**2- Ecriture de tests unitaires en suivant l'approche TDD :**
=====

Nous avons suivi l'approche TDD afin de créer la classe Order de la manière suivante.

- On développe un test qui échoue volontairement, mais qui respecte les règles métiers définies.
- On rédige le code qui permet de passer le test.
- On refactorise le code.

![image](https://user-images.githubusercontent.com/69635011/155520004-a4bf56dd-cd12-48cf-bef5-16f5203642ce.png)

```diff
- Pourquoi le TDD ?
```

Le TDD (Test Driven Development) est une pratique qui consiste à développer les tests avant d’écrire le code applicatif. De cette manière, les tests ne sont plus écrits en fonction du code, ce qui est le cas dans les processus actuels de développement. On évite ainsi de se retrouver dans la situation où, à cause de la manière dont l’application a été codée, on ne peut tester certaines parties.

Grâce au TDD, c’est au code de s’adapter aux tests, en travaillant de manière itérative jusqu’à arriver à un développement finalisé, testé à 100%


**3- Les tests unitaires sur les règles métier :**
=====

Les règles métier c'est quelque chose de très imortant dans un projet, que nous devons centraliser dans un seul endroit.

Afin d'assurer que personne ne peut violer ces règles métier il est également important de les tester une par une.

Notre projet se compose d'une entité Order qui comporte deux champs Customer et Pizza.

Order :
=

- Customer
- Pizza

Avant d'effectuer des tests sur la classe Order, on s'assure d'abord que les deux champs qui la composent sont bien testés
Par conséquent, nous avons testé les deux classes Customer et Pizza séparément, tout ça afin de créer une classe Order sur de bonnes bases !

```diff
! Les classes Customer et Pizza
```
![image](https://user-images.githubusercontent.com/69635011/155510971-013004c1-e5ec-45f9-a8de-a1c9c2a270fa.png)

```diff
! La classe Order
```
![image](https://user-images.githubusercontent.com/69635011/155511222-42ec4dda-1664-481b-911b-8fa78bcff711.png)


**4- Les tests d'intégration sur le CRUD :**
=====

```diff
! Customer
```
![image](https://user-images.githubusercontent.com/69635011/155513399-b97ddedc-148b-479b-9a40-9bc099d15ca8.png)

```diff
! Pizza
```
![image](https://user-images.githubusercontent.com/69635011/155513714-06c90237-0ba1-445c-895a-82d7dd145816.png)

**5- La couverture du code par les tests "Code Coverage" :**
=====


```diff
- 13 % : c'est le chiffre manquant afin que notre code soit couvert à 100% par des tests.

+ Pourquoi ?

- Les classes Program.cs et Startup.cs ne peuvent pas être incluses par la couverture de xUnit (pas d'appels directs aux fonctions). 

- Par conséquent, nous sommes plus dans les 88 %.

```

![Sans titre](https://user-images.githubusercontent.com/69635011/155516096-99efaad9-a669-4d00-96a0-154beb14b5a7.png)

![image](https://user-images.githubusercontent.com/69635011/155516305-1624a4bc-8fdb-4748-b1c1-251c8804ac54.png)


6- Le mutation testing, ou comment tester ses tests :
=====

Le principe est très simple. Il s’agit de rendre le code “malade” à l’aide de mutations et d’observer la capacité de nos tests à diagnostiquer l’anomalie introduite.

Les mutations appliquées au code peuvent être de différentes formes comme :

- la modification de la valeur d’une constante,
- le remplacement d’opérateurs,
- la suppression d’instructions,
et encore bien d’autres !

Si les tests restent au vert malgré les mutations du code, alors ils ne suffisent pas à détecter la régression amenée par le mutant.
On parle dans ce cas de mutations survivantes. A l’inverse, si au moins un test passe au rouge lors de l'exécution d’un code muté, alors la mutation est dite tuée (sous entendu par le test).

