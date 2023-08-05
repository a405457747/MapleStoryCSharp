-------------------------------------------------------
-- author : sky_allen
--  email : 894982165@qq.com
--   time : 2021/1/28 14:56:48
-------------------------------------------------------
require("functions");
require("sys")
require("ui")

UnityEngine = CS.UnityEngine;
GameObject = CS.UnityEngine.GameObject;
UI = CS.UnityEngine.UI;
Transform = CS.UnityEngine.Transform;
Vector3 = CS.UnityEngine.Vector3;
Vector2 = CS.UnityEngine.Vector2;
Time = CS.UnityEngine.Time;
Random = CS.UnityEngine.Random;
Quaternion = CS.UnityEngine.Quaternion;
Physics2D = CS.UnityEngine.Physics2D;
AudioSource = CS.UnityEngine.AudioSource;
Resources = CS.UnityEngine.Resources;
Color = CS.UnityEngine.Color;
SpriteRenderer = CS.UnityEngine.SpriteRenderer;

-- arr
function table.index_of(arr, val)

    for i, v in ipairs(arr) do
        if val == v then
            return i;
        end
    end

    return nil;
end

function table.find_all(arr, fn)
    local res = {};
    for i, v in ipairs(arr) do
        if fn(v) then
            res[#res + 1] = v;
        end
    end
    return res;
end

function table.contains(arr, val)
    for i, v in ipairs(arr) do
        if val == v then
            return true;
        end
    end
    return false;
end

function table.intersection(arr1, arr2)
    local res = {};
    table.sort(arr1);
    table.sort(arr2);

    local i, j = 1, 1;

    while ((i <= #arr1) and (j <= #arr2)) do
        if arr1[i] == arr2[j] then
            table.insert(res, arr1[i])
            i = i + 1;
            j = j + 1;
        elseif arr1[i] < arr2[j] then
            i = i + 1;
        else
            j = j + 1;
        end
    end
    return res;
end

function table.distinct(arr)
    local res = {};
    for i, v in ipairs(arr) do
        if not table.contains(res, v) then
            res[#res + 1] = v;
        end
    end
    return res;
end

function table.reverse(arr)
    local l = 1;
    local r = #arr;

    while l < r do
        arr[l], arr[r] = arr[r], arr[l];
        l = l + 1;
        r = r - 1;
    end
end

function table.add_range(arr, range)
    for i, v in ipairs(range) do
        arr[#arr + 1] = v;
    end
end

function table.clear(arr)
    while #arr > 0 do
        table.remove(arr);
    end
end

function table.sum(arr)
    local res = 0;
    for i, v in ipairs(arr) do
        res = res + v;
    end

    return res;
end

function table.foreach_operation(arr, fn)
    for i, v in ipairs(arr) do
        fn(arr, i);
    end
end

function table.print_arr(arr)
    local str = "";

    for i = 1, #arr do
        local interval = (i == #arr) and "" or " , ";
        str = str .. arr[i] .. interval;
    end

    print(str);
end

function table.print_nest_arr(nest_arr)
    for i, v in ipairs(nest_arr) do
        table.print_arr(v);
    end
end

function table.get_range(arr, start, count)
    local res = {};
    local endIndex = start + count - 1;
    for i = start, endIndex do
        res[#res + 1] = arr[i];
    end
    return res;
end

function table.insert_range(arr, start, range)
    table.reverse(range);
    for i, v in ipairs(range) do
        table.insert(arr, start, v);
    end
end

function table.remove_range(arr, start, count)
    local endIndex = start + count - 1;

    for i = start, endIndex do
        table.remove(arr, start);
    end
end

function table.swap(arr, i, j)
    arr[i], arr[j] = arr[j], arr[i];
end

function table.to_count_hash(arr)
    local hash = {}

    for i, v in ipairs(arr) do
        if hash[v] then
            hash[v] = hash[v] + 1;
        else
            hash[v] = 1;
        end
    end

    return hash;
end

function table.average(arr)
    return table.sum(arr) / #arr;
end

function table.prefix_sum(arr)
    local sum_arr = {};
    for i = 1, #arr do
        local prefix_arr = table.get_range(arr, 1, i);
        local val = table.sum(prefix_arr);
        sum_arr[i] = val;
    end

    return sum_arr;
end

function table.get_repeat(item_val, count)
    local res = {}

    for i = 1, count do
        res[i] = item_val;
    end

    return res;
end

function table.get_random_item(arr)
    return arr[math.random(#arr)];
end

-- hash
function table.print_hash(hash)

    if table.contains_key(hash, 1) then
        error("Don't support key is number 1.")
    end

    for i, v in pairs(hash) do
        print(i, v);
    end
end

function table.hash_count(hash)
    return #table.keys(hash);
end

function table.keys(hash)
    local res = {};
    for i, v in pairs(hash) do
        res[#res + 1] = i;
    end
    return res;
end

function table.values(hash)
    local res = {};
    for i, v in pairs(hash) do
        res[#res + 1] = v;
    end
    return res;
end

function table.contains_key(hash, key)
    return hash[key] ~= nil;
end

function table.contains_value(hash, val)
    local values = table.values(hash);
    return table.contains(values, val);
end

-------------------------------------

function string.value_of(str, index)
    return string.sub(str, index, index)
end

function string.index_of(str, char)
    for i = 1, #str do
        if string.value_of(str, i) == char then
            return i;
        end
    end
    return nil;
end

function string.to_char_arr(str)
    local res = {};
    for i = 1, #str do
        res[i] = string.value_of(str, i);
    end
    return res;
end

function string.serialize(obj)
    local lua = ""
    local t = type(obj)
    if t == "number" then
        lua = lua .. obj
    elseif t == "boolean" then
        lua = lua .. tostring(obj)
    elseif t == "string" then
        lua = lua .. string.format("%q", obj)
    elseif t == "table" then
        lua = lua .. "{\n"
        for k, v in pairs(obj) do
            lua = lua .. "[" .. string.serialize(k) .. "]=" .. string.serialize(v) .. ",\n"
        end
        local metatable = getmetatable(obj)
        if metatable ~= nil and type(metatable.__index) == "table" then
            for k, v in pairs(metatable.__index) do
                lua = lua .. "[" .. string.serialize(k) .. "]=" .. string.serialize(v) .. ",\n"
            end
        end
        lua = lua .. "}"
    elseif t == "nil" then
        return nil
    else
        error("can not serialize a " .. t .. " type.")
    end
    return lua
end

function string.unserialize(lua)
    local t = type(lua)
    if t == "nil" or lua == "" then
        return nil
    elseif t == "number" or t == "string" or t == "boolean" then
        lua = tostring(lua)
    else
        error("can not unserialize a " .. t .. " type.")
    end
    lua = "return " .. lua
    local func = loadstring(lua)
    if func == nil then
        return nil
    end
    return func()
end

function string.get_pure_number(str)
    local temp = string.gsub(str, "%D+", "");
    return tonumber(temp);
end

--------------------------

event = {
    container = {}
}

function event.add_listener(event_type, callback)
    if event.container[event_type] == nil then
        local callbacks = {};
        table.insert(callbacks, callback);
        event.container[event_type] = callbacks;
    else
        local callbacks = event.container[event_type];
        table.insert(callbacks, callback);
    end
end;

function event.remove_listener(event_type, callback)
    if event.container[event_type] == nil then
        error("The event type is nil");
    else
        local callbacks = event.container[event_type];
        for i, v in ipairs(callbacks) do
            if v == callback then
                table.remove(callbacks, i);
                break ;
            end
        end
    end
end

function event.broadcast(event_type, ...)
    local callbacks = event.container[event_type];

    for i, v in ipairs(callbacks) do
        v(...)
    end
end

function create_enum_table(tbl, idx)
    local res = {}
    local index = idx or 0

    for i, v in ipairs(tbl) do
        res[v] = index + i
    end
    return res
end


