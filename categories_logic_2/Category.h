#pragma once

#include <string>
#include <vector>
#include "Question.h"
#include <iostream>
#include <fstream>
#include "nlohmann/json.hpp"
using json = nlohmann::json;

struct ResultRange {
    int min_points;      // Минимальное количество баллов для этого результата
    int max_points;      // Максимальное количество баллов для этого результата
    std::string title;   // Заголовок результата (например, "Крош")
    std::string description; // Описание результата
    std::string image_path;  // Путь к изображению (опционально)

    // Конструктор по умолчанию
    ResultRange() : min_points(0), max_points(0), title(""), description(""), image_path("") {}

    // Конструктор с параметрами
    ResultRange(int min, int max, const std::string& t, const std::string& desc = "", const std::string& img = "")
        : min_points(min), max_points(max), title(t), description(desc), image_path(img) {
    }

    // Проверка, попадает ли количество баллов в диапазон
    bool contains(int points) const {
        return points >= min_points && points <= max_points;
    }

    bool edit(int min, int max, const std::string& t, const std::string& desc, const std::string& img) {
        bool changed = false;

        // Изменение текста вопроса
        if (min != min_points) {
            min_points = min;
            changed = true;
        }

        // Изменение вариантов ответов
        if (max!= max_points) {
           max_points = max;
            changed = true;
        }

        // Изменение правильных ответов
        if (!t.empty() && t != title) {
            title = t;
            changed = true;
        }

        // Изменение объяснения
        if (!desc.empty() && desc != description) {
            description = desc;
            changed = true;
        }
        if (!img.empty() && img != image_path) {
            image_path = img;
            changed = true;
        }

        return changed;
    }

    json toJson() const {
        json j;
        j["min_points"] = min_points;
        j["max_points"] = max_points;
        j["title"] = title;
        j["description"] = description;
        j["imape_path"] = image_path;
        return j;
    }
    void fromJson(const json& j) {
        min_points = j.value("min_points", 0);
        max_points = j.value("max_points", 0);
        title = j.at("title").get<std::string>();
        description = j.at("description").get<std::string>();
        image_path = j.at("image_path").get<std::string>();
    }
};

/**
 * Класс Category представляет категорию вопросов в викторине
 */
class Category {
private:
    std::string name_;              // Название категории
    std::vector<Question> questions_; // Массив вопросов категории
    bool is_finished_;               // Флаг завершения категории
    bool is_active_;                 // Флаг активности категории
    int currentQuestionIndex = 0;    //Флаг текущего вопроса
    std::string test_description_; // Описание категории (теста)
    std::vector<ResultRange> results_; // Список результатов
public:
    // Конструкторы
    Category();
    explicit Category(const std::string& name);
    explicit Category(const std::string& name, const std::string& test_description);
    ~Category();

    // Геттеры и сеттеры (опционально)
    std::string getName() const;
    void setName(const std::string& name);

    std::string getTestDescription() const;
    void setTestDescription(const std::string& test_description);

    bool isFinished() const;
    bool isActive() const;

    /**
     * Отображает информацию о категории
     */
    void showInfo();

    /**
     * Запускает категорию
     */
    void start();

    /**
     * Переходит к следующему вопросу
     */
    void nextQuestion();

    /**
     * Возвращается к предыдущему вопросу
     */
    void previousQuestion();

    /**
     * Завершает категорию и возвращает набранные очки
     * @return количество набранных очков
     */
    int end();

    /**
     * Добавляет новый вопрос в категорию
     */
    void addQuestion(const std::string& content,
        const std::vector<std::string>& options,
        const std::vector<int>& correct_options,
        int points,
        const std::string& explanation);

    /**
     * Удаляет вопрос по индексу
     * @param index индекс удаляемого вопроса
     */
    void removeQuestion(int index);

    /**
     * Загружает категорию из файла
     * @param filename имя файла для загрузки
     */
    json toJson() const;
    void fromJson(const json& j);

    // Перегруженные методы файлового ввода/вывода
    void loadFromFile(const std::string& filename);
    void saveToFile(const std::string& filename);

    // Для обратной совместимости (если нужны старые названия)
    void saveInFile(const std::string& filename) { saveToFile(filename); }
    bool validateJsonSchema(const json& j);
};