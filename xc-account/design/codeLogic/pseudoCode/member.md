### create

```java
public JSONObject create(){
    for(appIdList){
        if(sourceAppId != null && sourceAppId != null){
            if(sourceAppId下会员不存在){
                throw Exception;
            }
        }
        
        if(应用无效){
            throw Exception;
        }

        if(appId下会员不存在){
            创建会员

            if(有密码){
                设置盐值
                设置密码
            }
        }
    }

    设置会员的统一会员id

    return;
}
```

### upate
```java
public JSONObject update(){
    校验应用、会员的有效性

    if(已经绑过卡){  
        throw new Exception();  //绑过银行卡的会员不允许修改
    }

    updateMember();

    return;
}
```

### lockMember
```java
public JSONObject lockMember(){
    校验应用、会员的有效性

    修改会员状态

    return;
}
```

### unlockMember
```java
public JSONObject unlockMember(){
    校验应用、会员的有效性

    修改会员状态

    return;
}
```

### merge
```java
public JSONObject merge(fromMember, targetMember){
    
}

public void mergeMember(fromMember, targetMember){
    //合并交易密码
    if(targetMember的盐值不为空){
        mergeMemberPassword(targetMember.password, targetMember.salt, fromMember);
    }else if(fromMember的盐值不为空){
        mergeMemberPassword(fromMember.password, fromMember.salt, targetMember);
    }

    //修改场景会员的统一会员id
    获取统一会员id是fromMemberId, memberList

    for(memberList){
        修改member的统一会员id为targetMemberId
    }

    //合并会员银行卡
    把targetMember的银行卡绑到fromMember上
}

public void mergeMemberPassword(password, salt, walletMember){
    获取统一会员为walletMember的会员，memberList

    for(memberList){
        if(会员的盐值存在){
            更新盐值
            更新密码
            删除member缓存
        }
    }
}
```



