
// 차 이동방식
public enum e_move_type
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    NONE
}

// 캐릭터 이동방식 열거형
public enum e_character_move_type
{
    IDLE,
    UP    = 1,
    DOWN  = -1,
    LEFT  = -1,
    RIGHT = 1
};

public enum e_upgrade_menu_type
{
    CROP,
    CITY,
    RESIDENT,
    AUTOMATION,
    MAX
}

public enum e_multiplier_quantity // Multiplier button upgrade
{
    multiplier_per_one,
    multiplier_per_ten,
    multiplier_per_fifty
}