#pragma once

#include <string>
#include <vector>
#include <iostream>
#include <algorithm>
#include "nlohmann/json.hpp"
using json = nlohmann::json;

/**
 *  ласс Question представл€ет вопрос в викторине
 * ћожет содержать как простой текст, так и несколько вариантов ответа
 */
class Question {
private:
    // —одержание вопроса - может быть строкой или массивом строк (варианты ответов)
    std::string content_;  // текст вопроса
    std::vector<std::string> options_; // варианты ответов
    std::vector<int> points_options_; // ќчки за каждый из вариантов ответа
    bool is_resolved_;                    // ‘лаг решен ли вопрос
    int points_;                          //  оличество очков за вопрос (начисл€етс€ при ответе на вопрос)


public:
    //  онструкторы
    Question();
    explicit Question(const std::string& content);
    explicit Question(std::vector<std::string> options);
    explicit Question(const std::string& content,
        const std::vector<std::string>& options,
        const std::vector<int>& correct_options);
    // √еттеры и сеттеры
    std::string getContent() const;
    std::string getContentAsString() const;  // ѕолучить как единую строку
    void setContent(const std::string& content);

    bool isResolved() const;
    void setResolved(bool resolved);

    int getPoints() const;
    void setPoints(int points);

    /**
     * ѕодсчитывает количество очков за вопрос
     * @return количество начисленных очков
     */
    int countPoints();

    /**
     * –едактирует вопрос
     */
    bool edit(const std::string& newContent,
        const std::vector<std::string>& newOptions,
        const std::vector<int>& newCorrectOptions,
        int newPoints);


    /**
     * ƒобавл€ет вариант ответа 
     * @param option вариант ответа
     */
    void addOption(const std::string& option);

    /**
     * ”дал€ет вариант ответа
     * @param index индекс удал€емого варианта
     */
    void removeOption(int index);
    json toJson() const;
    void fromJson(const json& j);
};