# ShootingStar -流れボッシー-
Unityの個人製作(Android)

## はじめに
　このリポジトリ上のプロジェクトには、利用規約の関係で背景画像、音声素材とそのメタファイルは含まれておりません。あらかじめご了承ください。また、このプロジェクトはAndroidStudioを介してビルドしました。

## ゲーム説明  
　このプロジェクトは、星のボッシーを操作してひたすら障害物を避け続けて得点を稼いでいくゲームです。 
  
### タイトル画面
<img src="https://github.com/SamuraiOH/ShootingStar/assets/92005492/45db09f1-ed30-4f40-be48-510309dfd2a4" width="25%" />  
 
ゲーム起動時の画面。 
- 「スタート」ボタン  
  
  タップでゲームがスタートされます。 
- 「ゲーム説明」ボタン 
  
  押すと説明画面に切り替わり、ゲーム説明を見ることができます。
- ハイスコア

  過去の最高得点上位3位。(アプリごとにローカルで記録)

### 説明画面
<img src="https://github.com/SamuraiOH/ShootingStar/assets/92005492/ee5faa84-b225-4ccb-9da5-38b92d109cd1" width="25%" />  

ゲーム説明時の画面。タップするとタイトル画面に戻ります。  

### ゲーム画面
<img src="https://github.com/SamuraiOH/ShootingStar/assets/92005492/0bcc1094-d9dd-4c04-a161-b95af8134444" width="25%" />

ゲーム画面。
- ボッシー

![Bossy](https://github.com/SamuraiOH/ShootingStar/assets/92005492/acabc1cd-0353-4808-8600-8794268f53f1)

  プレイヤー。画面をスワイプするとその方向に応じて上下左右のいずれかに動き続けます。(上下は画面端で跳ね返り、左右は画面端で止まります。)
- スコア

![score](https://github.com/SamuraiOH/ShootingStar/assets/92005492/4ee7286c-f8d7-43c8-b7bc-e8e41750a984)

  プレイヤーの得点。基本的に0.1秒に1点加算されます。
- ライフ

![life](https://github.com/SamuraiOH/ShootingStar/assets/92005492/e9c555cd-7cbd-4e5e-9375-22830f8ca649)
  
  プレイヤーの体力。障害物に当たるたびに1つずつ減っていき、0になるとゲームオーバーです。
- ポーズボタン(右下)

![PauseButton](https://github.com/SamuraiOH/ShootingStar/assets/92005492/7d6928d5-7a56-4945-81f0-c7a99f688480)

  タップするとポーズ画面に移行し、ゲームが一時停止されます。

### 障害物
- ピラニア

![fish](https://github.com/SamuraiOH/ShootingStar/assets/92005492/ad583521-a7f1-48cb-adb0-4aaaa59a53e9)

　左側へまっすぐ突っ込んできます。

- 隕石

![stone](https://github.com/SamuraiOH/ShootingStar/assets/92005492/d35260b4-8461-4a7e-92d7-e94106cc234b)

　上から左下にまっすぐ落ちてきます。

- オクトパス

![octopus](https://github.com/SamuraiOH/ShootingStar/assets/92005492/3df0e949-dee7-4659-84be-c2543c87d042)

　動きはピラニアと同じですが、1回弾を撃ってきます。

- スキッド

![squid](https://github.com/SamuraiOH/ShootingStar/assets/92005492/4db6b7f1-8676-42cc-96c5-a97345a698ab)

　上下に蛇行しながら左に進んできます。

### アイテム
- バリアスター

![point](https://github.com/SamuraiOH/ShootingStar/assets/92005492/95623128-0b49-46cf-96cd-6ebd94d43081)

  時々右から左へ流れてきて、触れることでしばらくの間無敵になります。そして、その間は敵に当たって倒すことができ、そのたびにスコアが50加算されます。

- ハート

![heart](https://github.com/SamuraiOH/ShootingStar/assets/92005492/929e7c37-a21f-41aa-a8c4-4cc657593e59)

　時々右から左へ流れてきて、触れることでライフが1増えます。(最大5)

### ポーズ画面
<img src="https://github.com/SamuraiOH/ShootingStar/assets/92005492/979daabb-fbf8-4076-8a57-a2ec163c21cb" width="25%" /> 

ゲーム起動時の画面。 
- 「ゲームを再開する」ボタン  
  
  タップでゲームが再開されます。 
- 「ゲームをやめる」ボタン 
  
  押すとゲームを終了し、タイトル画面へ戻ります。(スコアの記録はされません。)

## 使用素材について
　背景画像とBGM、効果音、UIのフォントはフリーの素材を使用させていただいています。

### フォント
「Google Fonts」
https://fonts.google.com/specimen/RocknRoll+One/about?query=rock

- 「RocknRoll One」 designed by Fontworks Inc.
### 背景画像
「Unity Asset Store」
https://assetstore.unity.com/ja?locale=ja-JP

- 「Dynamic Space Background」 designed by DinV Studio
### BGM
「DOVA-SYNDROME」
https://dova-s.jp/

- 「Trick style」written by まんぼう三等兵
- 「追いかけっこキャッハー」 written by 今川彰人オーケストラ
### 効果音
「効果音ラボ」
https://soundeffect-lab.info/

- 「警告音1」
- 「警告音2」
- 「中パンチ」
- 「回復魔法1」
- 「ステータス上昇魔法2」
- 「ショット命中」
- 「キャンセル3」
- 「チーン1」
